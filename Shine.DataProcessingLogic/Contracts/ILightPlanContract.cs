using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using System;
using System.Linq;

namespace Shine.DataProcessingLogic.Contracts
{
    /// <summary>
    /// 光照计划服务接口
    /// </summary>
    public interface ILightPlanContract : IScopeDependency
    {
        /// <summary>
        /// <see cref="LightPlan"/>表预查询数据
        /// </summary>
        IQueryable<LightPlan> LightPlanQueryable { get; }

        /// <summary>
        /// 添加光照计划信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        OperationResult AddLightPlans(CacheUser cache, params LightPlanInputDto[] datas);

        /// <summary>
        /// 编辑光照计划信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        OperationResult EditLightPlans(CacheUser cache, params LightPlanInputDto[] datas);

        /// <summary>
        /// 删除光照计划信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        OperationResult DeleteLightPlans(CacheUser cache, params Guid[] Ids);

        /// <summary>
        /// 添加或更新标准光照计划数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        OperationResult LightPlan_0x54(LightPlan_0x54_In data);

        /// <summary>
        /// 添加或更新隧道光照计划数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        OperationResult LightPlan_0x59(LightPlan_0x59_In data);

        /// <summary>
        /// 更新主机标准光照计划-当前亮度值
        /// </summary>
        /// <param name="pack">主机注册包</param>
        /// <param name="brightness">当前亮度</param>
        /// <returns></returns>
        OperationResult LightPlan_0x56(string pack, int brightness);
    }
}
