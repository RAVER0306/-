using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Linq;

namespace Shine.DataProcessingLogic.Contracts
{
    /// <summary>
    /// 分组信息服务接口
    /// </summary>
    public interface IGroupControlContract : IScopeDependency
    {
        /// <summary>
        /// <see cref="GroupControl"/>表预查询数据
        /// </summary>
        IQueryable<GroupControl> GroupControlQueryable { get; }

        /// <summary>
        /// 添加分组信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        OperationResult AddGroupControls(CacheUser cache, params GroupControlInputDto[] datas);

        /// <summary>
        /// 编辑分组信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        OperationResult EditGroupControls(CacheUser cache, params GroupControlInputDto[] datas);

        /// <summary>
        /// 删除分组信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        OperationResult DeleteGroupControls(CacheUser cache, params Guid[] Ids);

        /// <summary>
        /// 更新或添加来自主机的分组信息
        /// </summary>
        /// <param name="data">分组信息数据</param>
        /// <returns></returns>
        OperationResult GroupControls_0x18(GroupControl_0x18_In data);
    }
}
