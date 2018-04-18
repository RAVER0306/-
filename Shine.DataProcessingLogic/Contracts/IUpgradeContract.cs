using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Contracts
{
    /// <summary>
    /// 升级服务接口
    /// </summary>
    public interface IUpgradeContract : IScopeDependency
    {
        /// <summary>
        /// 获取升级查询操作对象
        /// </summary>
        IQueryable<UpgradePackages> UpgradePackageQueryable { get;}

        IQueryable<UpgradeLog> UpgradeLogQueryable { get; }

        /// <summary>
        /// 添加升级包信息
        /// </summary>
        /// <param name="packages">主机升级包信息实体类</param>
        /// <returns></returns>
        OperationResult AddUpgradeFile(UpgradePackages packages);

        /// <summary>
        /// 删除指定升级包
        /// </summary>
        /// <param name="Id">升级包主键标识</param>
        /// <returns></returns>
        OperationResult DeleteUpgradeFile(Guid Id);

        /// <summary>
        /// 添加升级记录
        /// </summary>
        /// <param name="upgrade">升级记录信息</param>
        /// <returns></returns>
        OperationResult AddUpgradeLog(string[] guids, params UpgradeLog[] upgrades);

        /// <summary>
        /// 设置升级完成
        /// </summary>
        /// <returns></returns>
        OperationResult EditUpgradeLog(params dynamic[] dynamics);

        /// <summary>
        /// 删除升级记录
        /// </summary>
        /// <param name="cacheUser">缓存的操作用户</param>
        /// <param name="Id">指定的记录集合</param>
        /// <returns></returns>
        OperationResult DeleteUpgradeLog(CacheUser cacheUser, Guid[] Orgids, params Guid[] Ids);
    }
}
