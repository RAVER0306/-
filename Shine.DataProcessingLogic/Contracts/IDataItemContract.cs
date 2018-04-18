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
    public interface IDataItemContract : IScopeDependency
    {
        /// <summary>
        /// 表<see cref="DataItem"/>数据集查询
        /// </summary>
        IQueryable<DataItem> DataItemQueryable { get; }

        /// <summary>
        /// <see cref="DataItemDetail"/>数据集查询
        /// </summary>
        IQueryable<DataItemDetail> DataItemDetailQueryable { get; }

        /// <summary>
        /// 添加字典数据目录
        /// </summary>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        OperationResult AddDataItems(params DataItemInputDto[] datas);

        /// <summary>
        /// 根据主键集合删除字典目录
        /// </summary>
        /// <param name="Ids">待删除的字典目录主键集合</param>
        /// <returns></returns>
        OperationResult DeleteDataItems(params Guid[] Ids);

        /// <summary>
        /// 编辑字典目录
        /// </summary>
        /// <param name="datas">待更新的字典目录</param>
        /// <returns></returns>
        OperationResult EditDataItems(params DataItemInputDto[] datas);

        /// <summary>
        /// 添加字典内容
        /// </summary>
        /// <param name="datas">待添加的字典内容集合</param>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <returns></returns>
        OperationResult AddDataItemDetails(CacheUser cache, DataItemDetailInputDto[] datas);

        /// <summary>
        /// 编辑字典内容
        /// </summary>
        /// <param name="datas">待更新的字典内容集合</param>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <returns></returns>
        OperationResult EditDataItemDetails(CacheUser cache, DataItemDetailInputDto[] datas);

        /// <summary>
        /// 删除字典内容
        /// </summary>
        /// <param name="datas">待删除的字典内容主键集合</param>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <returns></returns>
        OperationResult DeleteDataItemDetails(CacheUser cache, params Guid[] Ids);
    }
}
