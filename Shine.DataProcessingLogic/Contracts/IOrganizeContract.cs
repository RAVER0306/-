using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface IOrganizeContract : IScopeDependency
    {
        /// <summary>
        /// 获取组织机构logo
        /// </summary>
        /// <param name="id">组织机构主键id</param>
        /// <returns></returns>
        byte[] GetOrganizeLogo(Guid id);

        /// <summary>
        /// 设置更新组织机构logo
        /// </summary>
        /// <param name="id">组织机构主键id</param>
        /// <param name="imageFile">准备要更新的组织机构logo文件信息</param>
        /// <returns></returns>
        Task<OperationResult> SetOrganizeLogo(Guid id, Task<FileInfo> imageFile);

        /// <summary>
        /// 获取<see cref="Organize"/>的数据源查询
        /// </summary>
        IQueryable<Organize> IQOrganizes{ get; }

        /// <summary>
        /// 获取<see cref="UserOrganizeMap"/>的数据源查询
        /// </summary>
        IQueryable<UserOrganizeMap> IQUserOrganizeMap { get; }

        /// <summary>
        /// 添加新的组织机构
        /// </summary>
        /// <param name="cache">当前操作的缓存用户</param>
        /// <param name="inputDto">信息输入模型</param>
        /// <returns></returns>
        Task<OperationResult> AddOrganizes(CacheUser cache,params OrganizeIn[] inputDto);

        /// <summary>
        /// 修改组织机构信息
        /// </summary>
        /// <param name="cache">当前操作的缓存用户</param>
        /// <param name="inputDto">信息输入模型</param>
        /// <returns></returns>
        Task<OperationResult> EditOrganizes(CacheUser cache, params OrganizeInputDto[] inputDto);

        /// <summary>
        /// 通过主键删除指定的组织机构
        /// </summary>
        /// <param name="Ids">组织机构主键集合</param>
        /// <returns></returns>
       Task<OperationResult> DeleteOrganizeById(params Guid[] Ids);
    }
}
