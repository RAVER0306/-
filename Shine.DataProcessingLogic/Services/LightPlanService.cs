using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Data;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Services
{
    public class LightPlanService : ILightPlanContract
    {
        #region 数据仓储
        /// <summary>
        /// <see cref="LightPlan"/>表数据仓储
        /// </summary>
        public IRepository<LightPlan, Guid> LightPlanRepository { set; get; }
        public IRepository<Host, Guid> HostRepository { set; get; }

        public IRepository<DataItemDetail, Guid> DataItemDetailRepository { set; get; }
        #endregion

        #region 接口实现
        /// <summary>
        /// <see cref="LightPlan"/>表预查询数据
        /// </summary>
        public IQueryable<LightPlan> LightPlanQueryable => LightPlanRepository.Entities;

        /// <summary>
        /// 添加光照计划信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        public OperationResult AddLightPlans(CacheUser cache, params LightPlanInputDto[] datas) => LightPlanRepository.Insert(datas,
                checkAction: dto =>
                {
                    if (LightPlanRepository.CheckExists(m => m.Host_Id == dto.Host_Id && m.DataItemDetail_Id == dto.DataItemDetail_Id))
                    {
                        throw new Exception($"id:主机主键:{dto.Host_Id} 的光照计划信息已经存在");
                    }
                },
                updateFunc: (dto, entity) =>
                 {
                     entity.CreatedTime = DateTime.Now;
                     entity.UpdatedTime = DateTime.Now;
                     return entity;
                 });

        /// <summary>
        /// 编辑光照计划信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        public OperationResult EditLightPlans(CacheUser cache, params LightPlanInputDto[] datas) => LightPlanRepository.Update(datas,
                checkAction: (dto, Entity) => { },
                updateFunc: (dto, entity) =>
                 {
                     entity.UpdatedTime = DateTime.Now;
                     return entity;
                 });

        /// <summary>
        /// 删除光照计划信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        public OperationResult DeleteLightPlans(CacheUser cache, params Guid[] Ids)
        {
            try
            {
                int count = 0;
                LightPlanRepository.UnitOfWork.BeginTransaction();
                foreach (var id in Ids)
                {
                    count += LightPlanRepository.DeleteDirect(id);
                }
                LightPlanRepository.UnitOfWork.Commit();
                return count > 0 ? new OperationResult(OperationResultType.Success, $"成功删除{count}条数据") :
                     new OperationResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"id:出现错误:{ex.Message}");
            }
        }

        /// <summary>
        /// 添加或更新标准光照计划数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public OperationResult LightPlan_0x54(LightPlan_0x54_In data)
        {
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == data.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"光照计划主机：{data.RegPackage}不存在");
            }
            else
            {
                var item = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "LigthPlanType" && m.Index == 0);
                if (item == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "标准光照计划类型主键不存在");
                }
                else
                {
                    var lg = LightPlanRepository.TrackEntities.FirstOrDefault(m => m.DataItemDetail_Id == item.Id && m.Host_Id == host.Id);
                    if (lg == null)
                    {
                        lg = data.MapTo<LightPlan>();                      
                        lg.CreatedTime = DateTime.Now;
                        lg.UpdatedTime = DateTime.Now;
                        lg.Host_Id = host.Id;
                        lg.DataItemDetail_Id = item.Id;
                        return LightPlanRepository.Insert(lg) > 0 ?
                            new OperationResult(OperationResultType.Success, $"主机:{data.RegPackage}的标准光照计划信息更新成功！") :
                            new OperationResult(OperationResultType.NoChanged, $"主机:{data.RegPackage}的标准光照计划信息更新未发生改变");
                    }
                    else
                    {
                        Guid Id = lg.Id;
                        lg = data.MapTo(lg);
                        lg.Id = Id;
                        lg.UpdatedTime = DateTime.Now;
                        return LightPlanRepository.Update(lg) > 0 ?
                          new OperationResult(OperationResultType.Success, $"主机:{data.RegPackage}的标准光照计划信息更新成功！") :
                          new OperationResult(OperationResultType.NoChanged, $"主机:{data.RegPackage}的标准光照计划信息更新未发生改变");
                    }
                }
            }
        }

        /// <summary>
        /// 添加或更新隧道光照计划数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public OperationResult LightPlan_0x59(LightPlan_0x59_In data)
        {
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == data.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"光照计划主机：{data.RegPackage}不存在");
            }
            else
            {
                var item = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "LigthPlanType" && m.Index == 1);
                if (item == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "隧道光照计划类型主键不存在");
                }
                else
                {
                    var lg = LightPlanRepository.TrackEntities.FirstOrDefault(m => m.DataItemDetail_Id == item.Id && m.Host_Id == host.Id);
                    if (lg == null)
                    {
                        lg = data.MapTo<LightPlan>();
                        lg.CreatedTime = DateTime.Now;
                        lg.UpdatedTime = DateTime.Now;
                        lg.Host_Id = host.Id;
                        lg.DataItemDetail_Id = item.Id;
                        return LightPlanRepository.Insert(lg) > 0 ?
                            new OperationResult(OperationResultType.Success, $"主机:{data.RegPackage}的隧道光照计划信息更新成功！") :
                            new OperationResult(OperationResultType.NoChanged, $"主机:{data.RegPackage}的隧道光照计划信息更新未发生改变");
                    }
                    else
                    {
                        Guid Id = lg.Id;
                        lg = data.MapTo(lg);
                        lg.Id = Id;
                        lg.UpdatedTime = DateTime.Now;
                        return LightPlanRepository.Update(lg) > 0 ?
                          new OperationResult(OperationResultType.Success, $"主机:{data.RegPackage}的隧道光照计划信息更新成功！") :
                          new OperationResult(OperationResultType.NoChanged, $"主机:{data.RegPackage}的隧道光照计划信息更新未发生改变");
                    }
                }
            }
        }

        /// <summary>
        /// 更新主机标准光照计划-当前亮度值
        /// </summary>
        /// <param name="pack">主机注册包</param>
        /// <param name="brightness">当前亮度</param>
        /// <returns></returns>
        public OperationResult LightPlan_0x56(string pack, int brightness)
        {
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == pack);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"主机:{pack}信息不存在");
            }
            else
            {
                var item = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "LigthPlanType" && m.Index == 0);
                if (item == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, $"光照计划类型主键信息不存在");
                }
                else
                {
                    var lg = LightPlanRepository.TrackEntities.FirstOrDefault(m => m.DataItemDetail_Id == item.Id && m.Host_Id == host.Id);
                    if (lg == null)
                    {
                        return new OperationResult(OperationResultType.QueryNull, $"主机：{pack} 当前光照计划对象不存在");
                    }
                    else
                    {
                        lg.CurrentBrightness = brightness;
                        lg.UpdatedTime = DateTime.Now;
                        return LightPlanRepository.Update(lg) > 0 ?
                          new OperationResult(OperationResultType.Success, $"主机: {pack} 当前光照度信息更新成功") :
                          new OperationResult(OperationResultType.NoChanged, $"主机: {pack} 当前光照度信息更新未发生改变");
                    }
                }
            }

        }
        #endregion
    }
}
