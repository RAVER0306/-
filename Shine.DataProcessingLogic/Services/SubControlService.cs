using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Logging;
using Shine.Core.Caching.Models;
using Shine.Core.Data;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.HostManager.Out;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Services
{
    public class SubControlService : ISubControlContract
    {
        #region 数据仓储

        public IRepository<Host, Guid> HostRepository { set; get; }

        /// <summary>
        /// <see cref="SubControl"/>表数据仓储
        /// </summary>
        public IRepository<SubControl, Guid> SubControlRepository { set; get; }

        /// <summary>
        /// <see cref="SubAggregation"/> 表的数据仓储
        /// </summary>
        public IRepository<SubAggregation, Guid> SubAggregationRepository { set; get; }

        /// <summary>
        /// <see cref="LightPole"/>表数据仓储
        /// </summary>
        public IRepository<LightPole, Guid> LightPoleRepository { set; get; }

        /// <summary>
        /// <see cref="SubRealTimeData"/>表数据仓储
        /// </summary>
        public IRepository<SubRealTimeData, Guid> SubRealTimeDataRepository { set; get; }

        /// <summary>
        /// <see cref="DataItemDetail"/>表数据仓储
        /// </summary>
        public IRepository<DataItemDetail, Guid> DataItemDetailRepository { set; get; }

        /// <summary>
        /// <see cref="AnnualElectricity"/>的数据仓储对象
        /// </summary>

        public IRepository<AnnualElectricity, Guid> AnnualElectricityRepository { set; get; }
        #endregion

        #region 接口实现
        /// <summary>
        /// <see cref="SubControl"/>表预查询数据
        /// </summary>
        public IQueryable<SubControl> SubControlQueryable => SubControlRepository.Entities;

        /// <summary>
        /// 获取分控分控上的灯具信息
        /// </summary>
        public IQueryable<SubRealTimeData> SubRealTimeDataQueryable => SubRealTimeDataRepository.Entities;

        /// <summary>
        /// <see cref="SubAggregation"/>表预查数据集合
        /// </summary>
        public IQueryable<SubAggregation> SubAggregationQueryable => SubAggregationRepository.Entities;

        /// <summary>
        /// 添加分控信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        public OperationResult AddSubControls(CacheUser cache, params SubControlInputDto[] datas)
        {
            int count = 0;
            SubAggregationRepository.UnitOfWork.BeginTransaction();
            foreach (var data in datas)
            {
                SubControl sub = data.MapTo<SubControl>();
                sub.CreatedTime = DateTime.Now;
                var poleone = LightPoleRepository.Entities.FirstOrDefault(m => m.Id == sub.LigthPoleOne_Id);
                if (poleone == null)
                {
                    throw new Exception("id:指定灯杆信息不存在");
                }
                if (SubControlRepository.CheckExists(m => m.SubNum == data.SubNum && m.LigthPoleOne.Host_Id == poleone.Host_Id))
                {
                    throw new Exception($"id:分控编号{data.SubNum}已经存在!不能在同一台主机上添加相同的分控编号");
                }
                if (poleone == null)
                {
                    throw new Exception("id:分控关联的灯杆不存在");
                }
                SubAggregation aggregation = new SubAggregation
                {
                    CreatedTime = DateTime.Now,
                    SubControlOne = sub,
                    LightPole_Id = sub.LigthPoleOne_Id,
                    Host_Id = poleone.Host_Id,
                    Organzie_Id = poleone.HostOne.Organize_Id
                };
                count += SubAggregationRepository.Insert(aggregation);
            };           
            SubAggregationRepository.UnitOfWork.Commit();
            return count > 0 ? new OperationResult(OperationResultType.Success, $"成功添加{count/2}条数据") :
            new OperationResult();
        }

        /// <summary>
        /// 编辑分控信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        public OperationResult EditSubControls(CacheUser cache, params SubControlInputDto[] datas) => SubControlRepository.Update(datas,
                checkAction: (dto, entity) => 
                {
                    if (dto.SubNum != entity.SubNum)
                    {
                        if (SubControlRepository.CheckExists(m => m.SubNum == dto.SubNum && m.LigthPoleOne_Id == dto.LigthPoleOne_Id))
                        {
                            throw new Exception($"id:分控编号{dto.SubNum}已经存在!不能在同一台主机上添加相同的分控编号");
                        }
                    }
                },
                updateFunc: (dto, entity) =>
                 {
                     var aggregation = SubAggregationRepository.Entities.FirstOrDefault(m=>m.SubControl_Id==entity.Id);
                     if (aggregation.LightPole_Id != entity.LigthPoleOne_Id)
                     {
                         var poleone = LightPoleRepository.Entities.FirstOrDefault(m => m.Id == entity.LigthPoleOne_Id);
                         if (poleone == null)
                         {
                             throw new Exception("id:分控关联的灯杆不存在");
                         }
                         aggregation.LightPole_Id = entity.LigthPoleOne_Id;
                         aggregation.Host_Id = poleone.Host_Id;
                         aggregation.Organzie_Id = poleone.HostOne.Organize_Id;
                         SubAggregationRepository.Update(aggregation);
                     }
                     return entity;
                 });

        /// <summary>
        /// 删除分控信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        public OperationResult DeleteSubControls(CacheUser cache, params Guid[] Ids)
        {
            try
            {
                int count = 0;
                SubControlRepository.UnitOfWork.BeginTransaction();
                foreach (var id in Ids)
                {
                    count += SubControlRepository.DeleteDirect(id);

                    // 删除分控关联的能耗信息
                    var ditem = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "ECType" && m.Index == 1);
                    var ect = AnnualElectricityRepository.TrackEntities.Where(m => m.ObjectId == id && m.DataItemDetail_Id == ditem.Id).ToList();
                    AnnualElectricityRepository.Delete(ect);

                }
                SubControlRepository.UnitOfWork.Commit();
                return count > 0 ? new OperationResult(OperationResultType.Success, $"成功删除{count}条数据") :
                    new OperationResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"id:出现错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 添加分控上的灯具基本信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <param name="datas">待添加的灯具</param>
        /// <returns></returns>
        public OperationResult AddSubReadTimeDatas(CacheUser cache, params SubRealTimeDataInputDto[] datas) => SubRealTimeDataRepository.Insert(datas,
                checkAction: dto => 
                {
                    if (!dto.DimmingPort.IsBetween(1, 2))
                    {
                        throw new Exception("id:灯具端口在1-2之间");
                    }
                    else if (SubRealTimeDataRepository.CheckExists(m => m.SubControl_Id == dto.SubControl_Id && m.DimmingPort == dto.DimmingPort))
                    {
                        throw new Exception($"Id:该分控上已经存在端口{dto.DimmingPort}的灯具!");
                    }
                },
                updateFunc: (dto, entity) =>
                {
                    entity.CreatedTime = DateTime.Now;
                    entity.UpdateTime = DateTime.Now;
                    return entity;
                });

        /// <summary>
        /// 编辑分控上的灯具基本信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <param name="datas">待添加的灯具</param>
        /// <returns></returns>
        public OperationResult EditSubReadTimeDatas(CacheUser cache, params SubRealTimeDataInputDto[] datas) => SubRealTimeDataRepository.Update(datas,
                checkAction: (dto, entity) =>{},
                updateFunc: (dto, entity) =>
                {
                    return entity;
                });

        /// <summary>
        /// 删除分控上的灯具基本信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        public OperationResult DeleteReadTimeDatas(CacheUser cache, params Guid[] Ids)
        {
            try
            {
                int count = 0;
                SubRealTimeDataRepository.UnitOfWork.BeginTransaction();
                foreach (var id in Ids)
                {
                  count +=  SubRealTimeDataRepository.DeleteDirect(id);
                }
                SubRealTimeDataRepository.UnitOfWork.Commit();
                return count > 0 ? new OperationResult(OperationResultType.Success, $"成功删除{count}条数据") :
                new OperationResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"id:出现错误：{ex.Message}");
            }

        }

        /// <summary>
        /// 更新分控的实时数据
        /// </summary>
        /// <param name="data_0X16_In">待更新的数据</param>
        /// <returns></returns>
        public string UpdateSubReadTimeData_0x16(params SubRealTimeData_0x16_In[] data_0X16_In)
        {
            try
            {
                int count = 0;
                SubRealTimeDataRepository.UnitOfWork.BeginTransaction();
                foreach (var dto in data_0X16_In)
                {
                    var value = SubRealTimeDataRepository.TrackEntities.FirstOrDefault(m => m.SubControlOne.SubNum == dto.SubNum &&
                                                             m.SubControlOne.LigthPoleOne.HostOne.RegPackage == dto.RegPackage
                                                             &&m.DimmingPort==dto.DimmingPort);

                    if (value != null)
                    {
                        Guid Id = value.Id;
                        var result = dto.MapTo(value);
                        result.Id = Id;
                        count += SubRealTimeDataRepository.Update(value);

                        #region 获取更新能耗信息
                        if (dto.EnergyConsumption > 0)
                        {
                            AddEnergyConsumption(value.SubControl_Id, value.SubControlOne.LigthPoleOne.HostOne.Organize_Id, dto.EnergyConsumption);
                        }
                        #endregion
                    }
                }
                SubRealTimeDataRepository.UnitOfWork.Commit();
                return count > 0 ? $"分控实时数据更新影响数据{count}条" :
                       $"提交的分控实时数据更新未发生任何改变";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 计算分控能耗数据
        /// </summary>
        /// <param name="SubId">分控主键</param>
        /// <param name="OrganizeId">分控所属的组织机构</param>
        /// <param name="value">能耗值</param>
        private void AddEnergyConsumption(Guid SubId, Guid OrganizeId, double value)
        {
            try
            {
                /*===================================================
                 * 备注：1、分控上传的能耗信息将一直累计
                 *       2、分控如果坏了，需更换则能耗清空
                 ===================================================*/

                // 获取能耗类型主键=='分控能耗类型'
                var ditem = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "ECType" && m.Index == 1);

                // 获取去年的能耗记录是否存在
                var _LastYearEct = AnnualElectricityRepository.TrackEntities
                                    .FirstOrDefault(m => m.ObjectId == SubId && m.DataItemDetail_Id == ditem.Id && m.Year == (DateTime.Now.Year - 1));
                // 获取当年的能耗记录是否存在
                var _CurrentYearEct = AnnualElectricityRepository.TrackEntities
                                    .FirstOrDefault(m => m.ObjectId == SubId && m.DataItemDetail_Id == ditem.Id && m.Year == DateTime.Now.Year);

                if (_CurrentYearEct == null/*当年的能耗记录为空，创建新的能耗记录*/)
                {
                    // 创建当前年的能耗记录
                    _CurrentYearEct = new AnnualElectricity
                    {
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        DataItemDetail_Id = ditem.Id,
                        Year = DateTime.Now.Year,
                        ObjectId = SubId,
                        Organzie_Id = OrganizeId,
                    };

                    // 创建当前月的能耗记录
                    MonthElectricity monthEct = new MonthElectricity
                    {
                        AnnualElectricityOne = _CurrentYearEct,
                        CreatedTime = DateTime.Now,
                        Month = DateTime.Now.Month,
                        UpdateTime = DateTime.Now
                    };

                    // 创建当前天的能耗记录
                    DayElectricity dayEct = new DayElectricity
                    {
                        UpdateTime = DateTime.Now,
                        CreatedTime = DateTime.Now,
                        MonthElectricityOne = monthEct,
                        Today = DateTime.Now.Day,
                    };

                    if (_LastYearEct == null/*去年能耗不存在，表示是全新的分控*/)
                    {
                        _CurrentYearEct.Cumulative = value;
                        _CurrentYearEct.YearTotal = value;
                        _CurrentYearEct[DateTime.Now.Month] = value;

                        monthEct.MonthTotal = value;
                        monthEct[DateTime.Now.Day] = value;

                        dayEct.DayTotal = value;
                        dayEct[DateTime.Now.Hour] = value;
                    }
                    else /*去年能耗存在，表示这是一个老分控，已经经过一年了，进行数据运算*/
                    {
                        if ((_LastYearEct.TagCumulative == -1 && _LastYearEct.Cumulative > value) || (_LastYearEct.TagCumulative != -1 && _LastYearEct.TagCumulative > value))
                        {   // 分控上传的累计能耗比存储数据的能耗记录小_CurrentYearEct
                            // 1.可能分控坏了，刚好过完一年更换了新的分控
                            _CurrentYearEct.YearTotal = value;
                            _CurrentYearEct.Cumulative += value;
                            _CurrentYearEct.TagCumulative = value; //分控被更换，计算新的累计值
                            _CurrentYearEct[DateTime.Now.Month] = value;

                            monthEct.MonthTotal = value;
                            monthEct[DateTime.Now.Day] = value;

                            dayEct.DayTotal = value;
                            dayEct[DateTime.Now.Hour] = value;
                        }
                        else
                        {
                            double xj = value - _LastYearEct.Cumulative;
                            if (_LastYearEct.TagCumulative != -1)
                            {
                                xj = value - _LastYearEct.TagCumulative;
                                
                            }
                            xj = xj > 0 ? xj : 0;
                            _CurrentYearEct.YearTotal = xj;
                            _CurrentYearEct.Cumulative = _LastYearEct.Cumulative + xj;
                            _CurrentYearEct.TagCumulative = _LastYearEct.TagCumulative+=xj;
                            _CurrentYearEct[DateTime.Now.Month] = xj;

                            monthEct.MonthTotal = xj;
                            monthEct[DateTime.Now.Day] = xj;

                            dayEct.DayTotal = xj;
                            dayEct[DateTime.Now.Hour] = xj;
                        }
                    }

                    // 关联能耗数据
                    monthEct.DayElectricityMany.Add(dayEct);
                    _CurrentYearEct.MonthElectricityMany.Add(monthEct);
                    AnnualElectricityRepository.Insert(_CurrentYearEct);
                }
                else /*==今年能耗存在，计算更新==*/
                {
                    var monthEct = _CurrentYearEct.MonthElectricityMany.FirstOrDefault(m => m.Month == DateTime.Now.Month);
                    if ((_CurrentYearEct.TagCumulative == -1 && _CurrentYearEct.Cumulative > value) || (_CurrentYearEct.TagCumulative != -1 && _CurrentYearEct.TagCumulative > value))
                    {
                        _CurrentYearEct.TagCumulative = value;
                        _CurrentYearEct.YearTotal += value;
                        _CurrentYearEct.Cumulative += value;
                        _CurrentYearEct[DateTime.Now.Month] += value;
                        _CurrentYearEct.UpdateTime = DateTime.Now;
                        if (monthEct == null)
                        {
                            monthEct = new MonthElectricity
                            {
                                Month = DateTime.Now.Month,
                                CreatedTime = DateTime.Now,
                                AnnualElectricityOne = _CurrentYearEct,
                                UpdateTime = DateTime.Now,
                                MonthTotal = value,
                            };
                            monthEct[DateTime.Now.Day] = value;

                            DayElectricity dayEct = new DayElectricity
                            {
                                UpdateTime = DateTime.Now,
                                CreatedTime = DateTime.Now,
                                DayTotal = value,
                                MonthElectricityOne = monthEct,
                                Today = DateTime.Now.Day,
                            };
                            dayEct[DateTime.Now.Hour] = value;

                            monthEct.DayElectricityMany.Add(dayEct);
                            _CurrentYearEct.MonthElectricityMany.Add(monthEct);
                            AnnualElectricityRepository.Update(_CurrentYearEct);
                        }
                        else
                        {
                            monthEct.Tag = DateTime.Now.Day;
                            monthEct.LastTagValue = monthEct.MonthTotal;
                            monthEct.MonthTotal += value;
                            monthEct[DateTime.Now.Day] += value;
                            monthEct.UpdateTime = DateTime.Now;

                            var dayEct = monthEct.DayElectricityMany.FirstOrDefault(m => m.Today == DateTime.Now.Day);
                            if (dayEct == null)
                            {
                                dayEct = new DayElectricity
                                {
                                    DayTotal = value,
                                    CreatedTime = DateTime.Now,
                                    MonthElectricityOne = monthEct,
                                    Today = DateTime.Now.Day,
                                    UpdateTime = DateTime.Now
                                };
                                dayEct[DateTime.Now.Hour] = value;
                                monthEct.DayElectricityMany.Add(dayEct);
                                AnnualElectricityRepository.Update(_CurrentYearEct);
                            }
                            else
                            {
                                dayEct.DayTotal += value;
                                dayEct.UpdateTime = DateTime.Now;
                                dayEct[DateTime.Now.Hour] += value;
                                AnnualElectricityRepository.Update(_CurrentYearEct);
                            }
                        }

                    }
                    else
                    {
                        double ob = value - _CurrentYearEct.Cumulative;
                        if (_CurrentYearEct.TagCumulative != -1)
                        {
                            ob = value - _CurrentYearEct.TagCumulative;
                            _CurrentYearEct.TagCumulative = value;
                        }
                        ob = ob > 0 ? ob : 0;
                        _CurrentYearEct[DateTime.Now.Month] += ob;
                        _CurrentYearEct.YearTotal += ob;
                        _CurrentYearEct.Cumulative += ob;
                        _CurrentYearEct.UpdateTime = DateTime.Now;

                        if (monthEct == null)
                        {
                            monthEct = new MonthElectricity
                            {
                                AnnualElectricityOne = _CurrentYearEct,
                                CreatedTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                Month = DateTime.Now.Month,
                                MonthTotal = ob,
                            };
                            monthEct[DateTime.Now.Day] = ob;

                            DayElectricity dayEct = new DayElectricity
                            {
                                UpdateTime = DateTime.Now,
                                CreatedTime = DateTime.Now,
                                MonthElectricityOne = monthEct,
                                Today = DateTime.Now.Day,
                                DayTotal = ob,
                            };
                            dayEct[DateTime.Now.Hour] = ob;

                            monthEct.DayElectricityMany.Add(dayEct);
                            _CurrentYearEct.MonthElectricityMany.Add(monthEct);
                            AnnualElectricityRepository.Update(_CurrentYearEct);

                        }
                        else
                        {
                            monthEct[DateTime.Now.Day] += ob;
                            monthEct.MonthTotal += ob;
                            monthEct.UpdateTime = DateTime.Now;

                            var dayEct = monthEct.DayElectricityMany.FirstOrDefault(m=>m.Today==DateTime.Now.Day);

                            if (dayEct == null)
                            {
                                dayEct = new DayElectricity
                                {
                                    DayTotal = ob,
                                    CreatedTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                    MonthElectricityOne = monthEct,
                                    Today = DateTime.Now.Day
                                };
                                dayEct[DateTime.Now.Hour] = ob;
                                monthEct.DayElectricityMany.Add(dayEct);
                                AnnualElectricityRepository.Update(_CurrentYearEct);
                            }
                            else
                            {
                                dayEct[DateTime.Now.Hour] += ob;
                                dayEct.DayTotal += ob;
                                dayEct.UpdateTime = DateTime.Now;
                                AnnualElectricityRepository.Update(_CurrentYearEct);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果报错，保存错误日志
                LogManager.GetLogger("AddEnergyConsumption_Sub").Error(ex);
            }
        }

        /// <summary>
        /// 更新分控的UID
        /// </summary>
        /// <param name="_0X29_In">数据</param>
        /// <returns></returns>
        public OperationResult UpdatedHost_0x29(SubControl_0x29_In _0X29_In)
        {
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == _0X29_In.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"分控所属主机：{_0X29_In.RegPackage} 不存在！");
            }
            else
            {
                var sub = SubControlRepository.TrackEntities.FirstOrDefault(m => m.SubNum == _0X29_In.SubNum && m.LigthPoleOne.Host_Id == host.Id);
                if (sub == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, $"主机：{_0X29_In.RegPackage} 分控：{_0X29_In.SubNum} 信息不存在");
                }else
                {
                    sub.UID = _0X29_In.UID;
                    return SubControlRepository.Update(sub) > 0 ?
                          new OperationResult(OperationResultType.Success, $"主机:{_0X29_In.RegPackage} 分控:{_0X29_In.SubNum} UID更新成功！") :
                          new OperationResult(OperationResultType.NoChanged, $"主机:{_0X29_In.RegPackage} 分控:{_0X29_In.SubNum} UID更新未发生改变！");
                }
            }
        }
        #endregion

        #region 编辑分控的经纬度
        /// <summary>
        /// 编辑分控的经纬度
        /// </summary>
        /// <param name="Id">分控主键</param>
        /// <param name="Longitude">精度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        public OperationResult EidtSubLocation(Guid Id, double Longitude, double Latitude)
        {
            int count = 0;
            var value = SubControlRepository.TrackEntities.FirstOrDefault(m=>m.Id==Id);
            if (value != null)
            {
                value.Longitude = Longitude;
                value.Latitude = Latitude;
                count += SubControlRepository.Update(value);
            }
            return count > 0 ? new OperationResult(OperationResultType.Success, $"更新分控经纬度成功，影响行数{count}") :
                  new OperationResult();
        }
        #endregion

        #region 
        /// <summary>
        /// 获取指定组织机构的分控数量
        /// </summary>
        /// <param name="Ids">指定的组织机构</param>
        /// <returns></returns>
        public OperationResult GetSubCount(params Guid[] Ids)
        {
            var num = SubControlRepository.Entities.Where(m => Ids.Contains(m.LigthPoleOne.HostOne.Organize_Id))?.Count();
            return new OperationResult(OperationResultType.Success, "获取数据成功", num); 
        }

        /// <summary>
        /// 获取指定组织机构的灯具数量
        /// </summary>
        /// <param name="Ids">指定的组织机构</param>
        /// <returns></returns>
        public OperationResult GetLightCount(params Guid[] Ids)
        {
            var num = SubRealTimeDataRepository.Entities.Where(m => Ids.Contains(m.SubControlOne.LigthPoleOne.HostOne.Organize_Id))?.Count();
            return new OperationResult(OperationResultType.Success, "获取数据成功", num);
        }

        /// <summary>
        /// 获取掉线分控数据
        /// </summary>
        /// <returns></returns>
        public List<EmailSubOffines> GetOffineSubs()
        {
            try
            {
                var data = SubControlRepository.UnitOfWork.SqlQuery<EmailSubOffines>("SELECT * FROM View_SubOffines").ToList();
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
