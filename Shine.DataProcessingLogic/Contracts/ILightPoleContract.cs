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
    /// 灯杆注册服务接口
    /// </summary>
    public interface ILightPoleContract : IScopeDependency
    {
        /// <summary>
        /// <see cref="LightPole"/>表预查询数据
        /// </summary>
        IQueryable<LightPole> LightPoleQueryable {  get; }

        /// <summary>
        /// 添加灯杆信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        OperationResult AddLightPoles(CacheUser cache, params LightPoleInputDto[] datas);

        /// <summary>
        /// 编辑灯杆信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        OperationResult EditLightPoles(CacheUser cache, params LightPoleInputDto[] datas);

        /// <summary>
        /// 删除灯杆信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        OperationResult DeleteLightPoles(CacheUser cache, params Guid[] Ids);
    }
}
