using Shine.DataProcessingLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.Core.Data;
using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.Comman.Logging;
using Shine.DataProcessingLogic.Dtos.HostManager.Out;

namespace Shine.DataProcessingLogic.Services
{
    public class HostService : IHostContract
    {
        #region 仓储对象
        /// <summary>
        /// 主机基本信息表<see cref="Host"/>数据仓储对象
        /// </summary>
        public IRepository<Host, Guid> HostRepository { set; get; }

        /// <summary>
        /// /<see cref="HostLogin"/>的信息仓储对象
        /// </summary>
        public IRepository<HostLogin, Guid> HostLoginRepository { set; get; }

        /// <summary>
        /// <see cref="HostParameter"/>的信息仓储对象
        /// </summary>
        public IRepository<HostParameter, Guid> HostParameterRepository { set; get; }

        /// <summary>
        /// <see cref="HostRealTimeData"/>的信息仓储对象
        /// </summary>
        public IRepository<HostRealTimeData, Guid> HostRealTimeDataRepository { set; get; }

        /// <summary>
        /// <see cref="DataItemDetail"/>的数据仓储对象
        /// </summary>
        public IRepository<DataItemDetail, Guid> DataItemDetailRepository { set; get; }

        /// <summary>
        /// <see cref="GroupControl"/>的数据仓储对象
        /// </summary>
        public IRepository<GroupControl, Guid> GroupControlRepository { set; get; }

        /// <summary>
        /// <see cref="AnnualElectricity"/>的数据仓储对象
        /// </summary>

        public IRepository<AnnualElectricity, Guid> AnnualElectricityRepository { set; get; }
        
        public IRepository<SubControl, Guid> SubControlRepository { get; set; }

        public IRepository<SubRealTimeData, Guid> SubRealTimeDataRepository { set; get; }
        #endregion

        #region 实现接口

        #region 获取主机查询数据集合
        /// <summary>
        /// 获取主机基本信息数据查询源
        /// </summary>
        public IQueryable<Host> HostQueryable => HostRepository.Entities;

        /// <summary>
        ///<see cref="HostLogin"/> 查询操作集合
        /// </summary>
        public IQueryable<HostLogin> HostLoginQueryable => HostLoginRepository.Entities;

        /// <summary>
        /// <see cref="HostParameter"/>查询操作集合
        /// </summary>
        public IQueryable<HostParameter> HostParameterQueryable => HostParameterRepository.Entities;
        #endregion

        #region 获取主机实时数据查询
        /// <summary>
        /// 获取主机实时数据查询
        /// </summary>
        public IQueryable<HostRealTimeData> HostRealTimeDataHostQueryable => HostRealTimeDataRepository.Entities;
        #endregion

        #region 添加新的主机
        /// <summary>
        /// 添加新的主机信息
        /// </summary>
        /// <param name="cache">当前登录用户的缓存</param>
        /// <param name="belongOrganizeId">当前用户所管理的组织机构ID</param>
        /// <param name="dtos">输入主机信息实体</param>
        /// <returns></returns>
        public OperationResult AddHosts(CacheUser cache, Guid[] belongOrganizeId, params HostInputDto[] dtos) => HostRepository.Insert(dtos,
                checkAction: m =>
                {
                    if (!cache.IsAdministrator)
                    {
                        if (!belongOrganizeId.Contains(m.Organize_Id))
                        {
                            throw new Exception($"id:主机{m.FullName}&{m.RegPackage}归属组织机构错误！");
                        }

                    }
                    if (HostRepository.CheckExists(a => a.RegPackage == m.RegPackage))
                    {
                        throw new Exception($"id:主机{m.RegPackage}已经存在");
                    }
                },
                updateFunc: (dto, entity) =>
                {
                    entity.CreatedTime = DateTime.Now;
                    entity.HostRealTimeDataMany.Add(new HostRealTimeData
                    {
                        //在主机实时数据表添加数据
                        HostOne = entity,
                        UpdateTime = DateTime.Now,
                    });
                    return entity;
                });
        #endregion

        #region 修改用户获取的主机信息
        /// <summary>
        /// 修改主机基本信
        /// </summary>
        /// <param name="cachePageId">当前用户获取的主机列表缓存页主键</param>
        /// <param name="dtos">输入信息实体模型</param>
        /// <returns></returns>
        public OperationResult EditHosts(Guid[] cachePageId, params HostInputDto[] dtos) => HostRepository.Update(dtos,
                checkAction: (dto, entity) =>
                {
                    if (!(cachePageId.Contains(dto.Id)))
                    {
                        throw new Exception($"id:主机{dto.FullName}&{dto.RegPackage}不存在或你未获取过该数据！");
                    }
                },
                updateFunc: (dto, entity) =>
                {
                    //var value = dto.MapTo<Host>();
                    //value.CreatedTime = entity.CreatedTime;
                    return entity;
                });
        #endregion

        #region 删除用户已获取的主机列表中的数据
        /// <summary>
        /// 删除用户已获取的主机列表中的数据
        /// </summary>
        /// <param name="cachePageId">当前用户获取的主机列表缓存页主键</param>
        /// <param name="Ids">准备被删除的主键集合</param>
        /// <returns></returns>
        public OperationResult DeleteHosts(Guid[] cachePageId, params Guid[] Ids)
        {
            int count = 0;
            HostRepository.UnitOfWork.BeginTransaction();
            foreach (var i in Ids)
            {
                if (!cachePageId.Contains(i))
                {
                    throw new Exception($"id:主机{i}不存在或你未获取过该数据！");
                }

                count += HostRepository.DeleteDirect(i);

                // 删除关联该主机的分组信息
                var ditem1 = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "GroupType" && m.Index == 0);
                var grous = GroupControlRepository.TrackEntities.Where(m => m.DataItemDetail_Id == ditem1.Id && m.ObjectId == i).ToList();
                GroupControlRepository.Delete(grous);

                // 删除主机关联的能耗信息
                var ditem2 = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "ECType" && m.Index == 0);
                var ect = AnnualElectricityRepository.TrackEntities.Where(m => m.DataItemDetail_Id == ditem2.Id && m.ObjectId == i).ToList();
                AnnualElectricityRepository.Delete(ect);

            }
            HostRepository.UnitOfWork.Commit();
            if (count > 0)
            {
                return new OperationResult(OperationResultType.Success, $"{count}条数据被删除");
            }
            else
            {
                return new OperationResult(OperationResultType.NoChanged, "请求被取消");
            }
        }
        #endregion

        #region 更新主机的实时数据
        /// <summary>
        /// 更新主机实时数据
        /// </summary>
        /// <param name="datas">待更新的数据</param>
        /// <returns></returns>
        public  string UpdatedHostTimeDatas(params HostRealTimeDataInputDto[] datas)
        {
            try
            {
                int count = 0;
                HostRealTimeDataRepository.UnitOfWork.BeginTransaction();
                foreach (var d in datas)
                {
                    var value = HostRealTimeDataRepository.TrackEntities.FirstOrDefault(m => m.HostOne.RegPackage == d.RegPackage);
                    if (value != null)
                    {
                        Guid Id = value.Id;
                        var result = d.MapTo(value);
                        result.Id = Id;
                        result.IsOnline = true;
                        count += HostRealTimeDataRepository.Update(result);
                        if (d.EnergyConsumption != 0 && d.EnergyConsumption != -1)
                        {
                            AddEnergyConsumption(value.Host_Id, value.HostOne.Organize_Id, d.EnergyConsumption);
                        }
                    }
                }
                HostRealTimeDataRepository.UnitOfWork.Commit();
                return count > 0 ? $"主机实时数据更新影响数据{count}条" :
                     "提交的主机实时数据更新未发生任何改变";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 添加或更新主机的能耗值
        /// </summary>
        /// <param name="HostId">主机主键</param>
        /// <param name="OrganizeId">主机所属的组织机构Id</param>
        /// <param name="value">指定能耗值</param>
        private void AddEnergyConsumption(Guid HostId, Guid OrganizeId, double value)
        {
            try
            {
                /*===================================================
                 * 备注：1、主机上传的能耗信息按月统计，每月清零
                 *       2、主机上传的能耗值小于记录中的值，代表主机能耗可能被清零或更换
                 ===================================================*/
                // 获取主机能耗类型主键==‘主机能耗类型’
                var ditem = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "ECType" && m.Index == 0);
                // 获取查询年能耗的查询仓储
                var _AnnualIQ = AnnualElectricityRepository.TrackEntities.Where(m => m.ObjectId == HostId && m.DataItemDetail_Id == ditem.Id);
                // 获取去年的年能耗记录
                var _LastYearEct = _AnnualIQ.FirstOrDefault(m => m.Year == (DateTime.Now.Year - 1));
                // 获取今年的能耗记录
                var _CurrentYeartEct = _AnnualIQ.FirstOrDefault(m => m.Year == DateTime.Now.Year);

                if (_CurrentYeartEct == null/*今年的能耗记录不存在，我们需要创建*/)
                {
                    // 按年统计，每月的能耗
                    AnnualElectricity annualElectricity = new AnnualElectricity
                    {
                        ObjectId = HostId,
                        DataItemDetail_Id = ditem.Id,
                        Year = DateTime.Now.Year,
                        UpdateTime = DateTime.Now,
                        CreatedTime = DateTime.Now,
                        Organzie_Id = OrganizeId,
                        YearTotal = value,
                    };
                    annualElectricity.Cumulative = value;
                    if (_LastYearEct != null/*去年能耗存在，则计算历史能耗累计数值*/)
                    {
                        annualElectricity.Cumulative += _LastYearEct.Cumulative;
                    }
                    annualElectricity[DateTime.Now.Month] = value; // 设置当前月的能耗

                    // 按月统计，每天的能耗
                    MonthElectricity monthElectricity = new MonthElectricity()
                    {
                        Month = DateTime.Now.Month,
                        CreatedTime = DateTime.Now,
                        AnnualElectricityOne = annualElectricity,
                        MonthTotal = value,
                        UpdateTime = DateTime.Now
                    };
                    monthElectricity[DateTime.Now.Day] = value;

                    // 按天统计，每时的能耗
                    DayElectricity dayElectricity = new DayElectricity()
                    {
                        UpdateTime = DateTime.Now,
                        CreatedTime = DateTime.Now,
                        DayTotal = value,
                        MonthElectricityOne = monthElectricity,
                        Today = DateTime.Now.Day
                    };
                    dayElectricity[DateTime.Now.Hour] = value;

                    // 关联统计
                    monthElectricity.DayElectricityMany.Add(dayElectricity);
                    annualElectricity.MonthElectricityMany.Add(monthElectricity);
                    AnnualElectricityRepository.Insert(annualElectricity);
                }
                else/*今年的能耗记录存在,则直接计算能耗差值*/
                {
                    // 获取本月的能耗记录
                    var _CurrentMonthEct = _CurrentYeartEct.MonthElectricityMany.FirstOrDefault(m => m.Month == DateTime.Now.Month);
                    if (_CurrentMonthEct == null/*当月能耗记录不存在*/)
                    {
                        _CurrentYeartEct.Cumulative += value; // 历史累计能耗计算
                        _CurrentYeartEct.YearTotal += value;  // 年总能耗计算

                        // 创建当前月的能耗统计
                        _CurrentMonthEct = new MonthElectricity
                        {
                            UpdateTime = DateTime.Now,
                            CreatedTime = DateTime.Now,
                            MonthTotal = value,
                            AnnualElectricityOne = _CurrentYeartEct,
                            Month = DateTime.Now.Month,
                        };
                        _CurrentMonthEct[DateTime.Now.Day] = value;

                        // 创建当前天的能耗统计
                        var _CurrentDayEct = new DayElectricity
                        {
                            UpdateTime = DateTime.Now,
                            CreatedTime = DateTime.Now,
                            DayTotal = value,
                            MonthElectricityOne = _CurrentMonthEct,
                            Today = DateTime.Now.Day
                        };
                        _CurrentDayEct[DateTime.Now.Hour] = value;

                        // 关联能耗统计记录
                        _CurrentMonthEct.DayElectricityMany.Add(_CurrentDayEct);
                        _CurrentYeartEct.MonthElectricityMany.Add(_CurrentMonthEct);
                        AnnualElectricityRepository.Update(_CurrentYeartEct);
                    }
                    else/*当月能耗存在*/
                    {
                        // 获取当天的能耗记录
                        var _CurrentDayEct = _CurrentMonthEct.DayElectricityMany.FirstOrDefault(m => m.Today == DateTime.Now.Day);

                        // 上传的能耗数据比记录中的小，则主机能耗被清零操作或主机被更换
                        if (_CurrentYeartEct[DateTime.Now.Month] > value)
                        {
                            double overs = Math.Abs(value - (_CurrentMonthEct.MonthTotal - _CurrentMonthEct.LastTagValue));
                            if (_CurrentMonthEct.Tag == 0)
                            {
                                _CurrentMonthEct.Tag = DateTime.Now.Day;
                                _CurrentMonthEct.LastTagValue = _CurrentMonthEct.MonthTotal;
                                overs = value;
                            }
                            else
                            {
                                if ((_CurrentMonthEct.MonthTotal-_CurrentMonthEct.LastTagValue) > value)
                                {
                                    _CurrentMonthEct.Tag = DateTime.Now.Day;
                                    _CurrentMonthEct.LastTagValue = _CurrentMonthEct.MonthTotal;
                                    overs = value;
                                }
                            }
                            if (_CurrentDayEct == null)
                            {
                                // 创建当前天的能耗统计
                                _CurrentDayEct = new DayElectricity
                                {
                                    UpdateTime = DateTime.Now,
                                    CreatedTime = DateTime.Now,
                                    MonthElectricityOne = _CurrentMonthEct,
                                    Today = DateTime.Now.Day
                                };
                                _CurrentMonthEct.DayElectricityMany.Add(_CurrentDayEct);
                                //if (boolRp) { overs = value; }
                                _CurrentMonthEct[DateTime.Now.Day] = overs;
                                _CurrentDayEct[DateTime.Now.Hour] = overs;
                            }
                            else
                            {
                                _CurrentMonthEct[DateTime.Now.Day] += overs;
                                double xc = _CurrentMonthEct[DateTime.Now.Day];
                                for (int n = 0; n < DateTime.Now.Hour; n++)
                                {
                                    xc -= _CurrentDayEct[n];
                                }
                                _CurrentDayEct[DateTime.Now.Hour] = Math.Abs(xc);
                            }
                            _CurrentDayEct.DayTotal = _CurrentMonthEct[DateTime.Now.Day];
                            _CurrentMonthEct.MonthTotal += overs;
                            _CurrentYeartEct[DateTime.Now.Month] += overs;
                            _CurrentYeartEct.YearTotal += overs;
                            _CurrentYeartEct.Cumulative += overs;
                            AnnualElectricityRepository.Update(_CurrentYeartEct);
                        }
                        else
                        {
                            _CurrentYeartEct.YearTotal += (value - _CurrentYeartEct[DateTime.Now.Month]);
                            _CurrentYeartEct.Cumulative+= (value - _CurrentYeartEct[DateTime.Now.Month]);
                            _CurrentMonthEct.MonthTotal = value;
                            _CurrentYeartEct[DateTime.Now.Month] = value;
                            Double xf = value;
                            for (int n = 1; n < DateTime.Now.Day; n++)
                            {
                                xf -= _CurrentMonthEct[n];
                            }
                            _CurrentMonthEct[DateTime.Now.Day] = Math.Abs(xf);
                            _CurrentDayEct.DayTotal= Math.Abs(xf);
                            double xc = Math.Abs(xf);
                            for (int n = 0; n < DateTime.Now.Hour; n++)
                            {
                                xc -= _CurrentDayEct[n];
                            }
                            _CurrentDayEct[DateTime.Now.Hour] = Math.Abs(xc);
                            AnnualElectricityRepository.Update(_CurrentYeartEct);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果报错，保存错误日志
                LogManager.GetLogger("AddEnergyConsumption_Host").Error(ex);
            }
        }
        #endregion

        #region 更新主机应答信息
        /// <summary>
        /// 更新主机应答信息
        /// </summary>
        /// <param name="_0X61_In">数据</param>
        /// <returns></returns>
        public OperationResult UpdatedHost_0x61(Host_0x61_In _0X61_In)
        {
            var value =HostRealTimeDataRepository .TrackEntities.FirstOrDefault(m => m.HostOne.RegPackage == _0X61_In.RegPackage);
            if (value != null)
            {
                value.Latitude = _0X61_In.Latitude;
                value.Longitude = _0X61_In.Longitude;
                value.TimeZone = _0X61_In.TimeZone;
                value.UpdateTime = DateTime.Now;
                return HostRealTimeDataRepository.Update(value) > 0 ?
                       new OperationResult(OperationResultType.Success, $"主机：{_0X61_In.RegPackage} 请求应答的参数更新成功！") :
                       new OperationResult(OperationResultType.NoChanged, $"主机：{_0X61_In.RegPackage} 请求应答的参数更新未发生改变！");
            }
            else
            {
                return new OperationResult(OperationResultType.QueryNull, $"主机：{_0X61_In.RegPackage} 不存在");
            }
        }
        #endregion

        #region 更新主机系统参数数据
        /// <summary>
        /// 主机系统参数更新
        /// </summary>
        /// <param name="_0X25_In">数据</param>
        /// <returns></returns>
        public OperationResult UpdatedHostParameter_0x25(HostParameter_0x25_In _0X25_In)
        {
            var host = HostRepository.Entities.FirstOrDefault(m=>m.RegPackage==_0X25_In.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"主机：{_0X25_In.RegPackage} 信息不存在");
            }
            else
            {
                var parameter =  HostParameterRepository.TrackEntities.FirstOrDefault(m => m.Host_Id == host.Id);
                if (parameter == null)
                {
                    parameter = _0X25_In.MapTo<HostParameter>();
                    parameter.Host_Id = host.Id;
                    parameter.UpdateTime = DateTime.Now;
                    return HostParameterRepository.Insert(parameter) > 0 ?
                         new OperationResult(OperationResultType.Success, $"主机：{_0X25_In.RegPackage} 系统参数更新成功！") :
                         new OperationResult(OperationResultType.NoChanged, $"主机：{_0X25_In.RegPackage} 系统参数更新未发生改变！");
                }
                else
                {
                    Guid id = parameter.Id;
                    parameter = _0X25_In.MapTo(parameter);
                    parameter.Id = id;
                    parameter.UpdateTime = DateTime.Now;
                    return HostParameterRepository.Update(parameter) > 0 ?
                         new OperationResult(OperationResultType.Success, $"主机：{_0X25_In.RegPackage} 系统参数更新成功！") :
                         new OperationResult(OperationResultType.NoChanged, $"主机：{_0X25_In.RegPackage} 系统参数更新未发生改变！");
                }
            }
        }
        #endregion

        #region 更新主机登陆信息
        /// <summary>
        /// 主机登陆信息数据更新
        /// </summary>
        /// <param name="_0X44_In">数据</param>
        /// <returns></returns>
        public OperationResult UpdatedHostLogin_0x44(HostLogin_0x44_In _0X44_In)
        {
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == _0X44_In.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"主机：{_0X44_In.RegPackage} 信息不存在");
            }
            else
            {
                var hostlogin = HostLoginRepository.TrackEntities.FirstOrDefault(m => m.Host_Id == host.Id);
                if (hostlogin == null)
                {
                    hostlogin = _0X44_In.MapTo<HostLogin>();
                    hostlogin.Host_Id = host.Id;
                    hostlogin.UpdateTime = DateTime.Now;
                    return HostLoginRepository.Insert(hostlogin) > 0 ?
                           new OperationResult(OperationResultType.Success, $"主机：{_0X44_In.RegPackage} 登陆信息参数更新成功！") :
                           new OperationResult(OperationResultType.NoChanged, $"主机：{_0X44_In.RegPackage} 登陆信息参数更新未发生改变！");
                }
                else
                {
                    Guid id = hostlogin.Id;
                    hostlogin = _0X44_In.MapTo(hostlogin);
                    hostlogin.Id = id;
                    hostlogin.UpdateTime = DateTime.Now;
                    return HostLoginRepository.Update(hostlogin) > 0 ?
                         new OperationResult(OperationResultType.Success, $"主机：{_0X44_In.RegPackage} 登陆信息参数更新成功！") :
                         new OperationResult(OperationResultType.NoChanged, $"主机：{_0X44_In.RegPackage} 登陆信息参数更新未发生改变！");
                }
            }
        }
        #endregion

        #region 获取指定组织机构下的主机数量
        /// <summary>
        /// 获取指定组织机构下的所有主机
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public OperationResult GetHostCount(params Guid[] OrganizeId)
        {
            var HostNum = HostRepository.Entities.Where(m => OrganizeId.Contains(m.Organize_Id))?.Count();
            var SubNum = SubControlRepository.Entities.Where(m=>OrganizeId.Contains(m.LigthPoleOne.HostOne.Organize_Id))?.Count();
            var LightNum = SubRealTimeDataRepository.Entities.Where(m => OrganizeId.Contains(m.SubControlOne.LigthPoleOne.HostOne.Organize_Id))?.Count();
            return new OperationResult(OperationResultType.Success, "获取数据成功", new {HostNum,SubNum,LightNum });
        }
        #endregion

        /// <summary>
        /// 获取离线主机集合
        /// </summary>
        /// <returns></returns>
        public List<EmailHostOffines> GetOffineHosts()
        {
            try
            {
                var data = HostRepository.UnitOfWork.SqlQuery<EmailHostOffines>("SELECT * FROM View_HostOffines").ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
