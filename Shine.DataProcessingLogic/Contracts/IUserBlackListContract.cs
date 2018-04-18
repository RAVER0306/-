using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.UserManager;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface IUserBlackListContract : IScopeDependency
    {
        /// <summary>
        /// 获取黑名单信息查询集合
        /// </summary>
        IQueryable<UserBlackList> UBLQueryable { get; }

        /// <summary>
        /// 编辑黑名单
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待更改的数据集合</param>
        /// <returns></returns>
        OperationResult EditUserBlackLists(CacheUser cache, params UserBlackListInputDto[] datas);


        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        OperationResult AddUserBlackLists(CacheUser cache, params UserBlackListInputDto[] datas);

        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        OperationResult DeleteUserBlackLists(CacheUser cache, params Guid[] Ids);
    }
}
