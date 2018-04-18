using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using System;
using System.Linq;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface IHostPolicyContract : IScopeDependency
    {
        /// <summary>
        /// 获取主机策略查询操作集合
        /// </summary>
        IQueryable<HostPolicy> HostPolicyQueryable { get; }

        /// <summary>
        /// 获取主机策略日志查询的数据集
        /// </summary>
        IQueryable<HostPolicyLog> HostPolicyLogQueryable { get; }

        /// <summary>
        /// 添加主机策略
        /// </summary>
        /// <param name="cache">当前缓存的操作用户信息</param>
        /// <param name="dtos">输入实体</param>
        /// <returns></returns>
        OperationResult AddHostPolicys(CacheUser cache,params HostPolicyInputDto[] dtos);

        /// <summary>
        /// 更新主机策略
        /// </summary>
        /// <param name="cache">当前缓存的操作用户信息</param>
        /// <param name="dtos">输入实体</param>
        /// <returns></returns>
        OperationResult EditHostPolicys(Guid[] cachePageIds,CacheUser cache, params HostPolicyInputDto[] dtos);

        /// 删除主机信息
        /// </summary>
        /// <param name="ids">主机策略信息主键id集合</param>
        /// <returns></returns>
        OperationResult DeleteHostpolicys(Guid[] cachePageIds, params Guid[] ids);

        /// <summary>
        /// 删除策略日志
        /// </summary>
        /// <param name="ids">准备要删除的策略日志ID集合</param>
        /// <returns></returns>
        OperationResult DeleteHostPolicyLogs(params Guid[] ids);

        /// <summary>
        /// 更新指定主机的策略信息
        /// </summary>
        /// <param name="_0X5B_In">主机策略信息数据</param>
        /// <returns></returns>
        OperationResult Updataed_0x5B(HostPolicy_0x5B_In _0X5B_In);
    }
}
