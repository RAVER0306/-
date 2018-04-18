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
using Shine.Comman.Extensions;

namespace Shine.DataProcessingLogic.Services
{
    public class HostPolicyService : IHostPolicyContract
    {
        #region 数据仓储
        /// <summary>
        /// 表<see cref="HostPolicy"/>的数据仓储
        /// </summary>
        public IRepository<HostPolicy, Guid> HostPolicyRepository { get; set; }

        /// <summary>
        /// 表<see cref="Host"/>的数据仓储
        /// </summary>
        public IRepository<Host, Guid> HostRepository { set; get; }


        /// <summary>
        /// 表<see cref="HostPolicyLog"/>的数据仓储
        /// </summary>
        public IRepository<HostPolicyLog, Guid> HostPolicyLogRepository { set; get; }
        #endregion

        #region 实现接口
        /// <summary>
        /// 获取主机策略信息查询数据集
        /// </summary>
        public IQueryable<HostPolicy> HostPolicyQueryable => HostPolicyRepository.Entities;

        /// <summary>
        /// 获取主机策略日志查询的数据集
        /// </summary>
        public IQueryable<HostPolicyLog> HostPolicyLogQueryable => HostPolicyLogRepository.Entities;

        /// <summary>
        /// 添加主机策略
        /// </summary>
        /// <param name="cache">当前缓存的操作用户信息</param>
        /// <param name="dtos">输入实体</param>
        /// <returns></returns>
        public OperationResult AddHostPolicys(CacheUser cache, params HostPolicyInputDto[] dtos) => HostPolicyRepository.Insert(dtos,
               checkAction: dto =>
               {
                   if (!cache.Level.IsBetween(1, 2))
                   {
                       throw new Exception($"id:你的操作权限等级过低");
                   }
                   if (HostPolicyRepository.Entities.FirstOrDefault(m => m.Host_Id == dto.Host_Id && m.Number == dto.Number) != null)
                   {
                       throw new Exception("id:准备添加的策略信息已经存在");
                   }
               },
               updateFunc: (dto, entity) =>
               {
                   entity.CreatedTime = DateTime.Now;
                   entity.Organzie_Id = cache.Organize_Id;
                   entity.UpdateTime = DateTime.Now;
                   return entity;
               });

        /// <summary>
        /// 更新主机策略
        /// </summary>
        /// <param name="cache">当前缓存的操作用户信息</param>
        /// <param name="dtos">输入实体</param>
        /// <returns></returns>
        public OperationResult EditHostPolicys(Guid[] cachePageIds, CacheUser cache, params HostPolicyInputDto[] dtos) => HostPolicyRepository.Update(dtos,
               checkAction: (dto, Entity) =>
               {
                   if (!cache.Level.IsBetween(1, 2))
                   {
                       throw new Exception($"id:你的操作权限等级过低");
                   }
                   if (!cachePageIds.Contains(dto.Id))
                   {
                       throw new Exception($"id:数据不存在或你未获取数据编辑权限！");
                   }
               },
               updateFunc: (dto, entity) =>
               {
                   //var value = dto.MapTo<HostPolicy>();
                   //value.CreatedTime = entity.CreatedTime;
                   //value.Organzie_Id = entity.Organzie_Id;
                   //value.UpdateTime = DateTime.Now;
                   return entity;
               });

        /// <summary>
        /// 删除主机信息
        /// </summary>
        /// <param name="ids">主机策略信息主键id集合</param>
        /// <returns></returns>
        public OperationResult DeleteHostpolicys(Guid[] cachePageIds, params Guid[] ids)
        {
            HostPolicyRepository.UnitOfWork.BeginTransaction();
            int count = 0;
            foreach (var i in ids)
            {
                if (!cachePageIds.Contains(i))
                {
                    throw new Exception($"id:数据不存在或你未获取数据编辑权限！");
                }
                count += HostPolicyRepository.DeleteDirect(i);
            }
            HostPolicyRepository.UnitOfWork.Commit();
            if (count > 0)
            {
                return new OperationResult(OperationResultType.Success, $"删除{count}条数据！");
            }
            else
            {
                return new OperationResult();
            }
        }

        /// <summary>
        /// 删除下载策略日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public OperationResult DeleteHostPolicyLogs(params Guid[] ids)
        {
            HostPolicyLogRepository.UnitOfWork.BeginTransaction();
            int count = 0;
            foreach (var i in ids)
            {
                count += HostPolicyLogRepository.DeleteDirect(i);
            }
            HostPolicyLogRepository.UnitOfWork.Commit();
            if (count > 0)
            {
                return new OperationResult(OperationResultType.Success, $"成功删除{count}条数据");
            }
            else
            {
                return new OperationResult();
            }
        }

        /// <summary>
        /// 更新指定主机的策略信息
        /// </summary>
        /// <param name="_0X5B_In">主机策略信息数据</param>
        /// <returns></returns>
        public OperationResult Updataed_0x5B(HostPolicy_0x5B_In _0X5B_In)
        {
            //检查主机是否存在
            var host = HostRepository.Entities.FirstOrDefault(m=>m.RegPackage==_0X5B_In.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"主机：{_0X5B_In.RegPackage} 的数据对象不存在");
            }
            else
            {
                var obb = HostPolicyRepository.TrackEntities.FirstOrDefault(m => m.HostOne.RegPackage == _0X5B_In.RegPackage && m.Number == _0X5B_In.Number);
                if (obb == null)
                {
                    obb = _0X5B_In.MapTo<HostPolicy>();
                    obb.Host_Id = host.Id;
                    obb.CreatedTime = DateTime.Now;
                    obb.UpdateTime = DateTime.Now;
                    obb.FullName = $"Strategy:{obb.Number}";
                    obb.Organzie_Id = host.Organize_Id;
                    return HostPolicyRepository.Insert(obb) > 0 ?
                        new OperationResult(OperationResultType.Success, $"主机：{_0X5B_In.RegPackage} 的策略信息更新成功") :
                        new OperationResult(OperationResultType.NoChanged, $"主机：{_0X5B_In.RegPackage}的策略信息更新失败");
                }
                else
                {
                    Guid Id = obb.Id;
                    obb = _0X5B_In.MapTo(obb);
                    obb.Id = Id;
                    obb.UpdateTime = DateTime.Now;
                    return HostPolicyRepository.Update(obb) > 0 ?
                       new OperationResult(OperationResultType.Success, $"主机：{_0X5B_In.RegPackage} 的策略信息更新成功") :
                       new OperationResult(OperationResultType.NoChanged, $"主机：{_0X5B_In.RegPackage}的策略信息更新失败");
                }
            }          
        }
        #endregion
    }
}
