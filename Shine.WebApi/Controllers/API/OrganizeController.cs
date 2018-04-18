using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.OrganzieManager;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.DataProcessingLogic.Models.UserManager;
using Shine.Web.WebApi;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 组织机构接口
    /// </summary>
    [Description("组织机构相关信息管理接口")]
    public class OrganizeController : ThisBaseApiController
    {
        #region 用户头像图片获取
        /// <summary>  
        /// 获取组织机构logo
        /// </summary>  
        [HttpGet]
        [Route("Organize/{id}/OrganizeLog")]
        [Description("组织机构logo获取")]
        public HttpResponseMessage OrganizeLog(string id) => OrganizeService.TryCatchAction(
                action: m =>
                {
                    id.CheckNotNull("id");
                    Guid gid = new Guid(id.AESDecrypt128());
                    var imgByte = OrganizeService.GetOrganizeLogo(gid);
                    var resp = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(imgByte)
                    };
                    resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                    return resp;
                },
                failureAction: b =>
                 {
                     switch (b)
                     {
                         case Exception ex when (ex is ArgumentException || ex is ArgumentNullException):
                             return ex.CatchExceptionMsg(HttpStatusCode.ExpectationFailed);
                         case Exception ex when (ex.Message.Contains("id:")):
                             return ex.CatchExceptionMsg(HttpStatusCode.NoContent, isSubstring: true);
                         default:
                             return b.CatchExceptionMsg(HttpStatusCode.InternalServerError, unknown: true);

                     }
                 });
        #endregion

        #region 组织机构logo上传更新
        /// <summary>
        /// [upload]组织机构logo上传更新
        /// </summary>
        /// <param name="id">组织机构主键ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Organize/ImgUpload/{id}")]
        [Description("组织机构logo上传更新")]
        public async Task<IHttpActionResult> ImgUpload(Guid id) => Json(await OrganizeService.TryCatchActionAsync(
                 action: m =>
                 {
                     id.CheckNotEmpty("id");
                     if (!ListOrganizeId.Contains(id))
                     {
                         throw new Exception("id:你没有权限修改该组织的Logo");
                     }
                     return OrganizeService.SetOrganizeLogo(id, Request.SaveUploadFile(id.ToString().ToUpper(), SaveFileType.OrganizeLogo));
                 }));
        #endregion

        #region 获取当前登录用户的所属组织机构
        /// <summary>
        /// 获取当前登录用户的所属组织机构
        /// </summary>
        /// <param name="Id">组织机构Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Organize/{id}")]
        [Description("获取当前登录用户的所属组织机构")]
        public IHttpActionResult GetOrganizeById(Guid id) => Json(OrganizeService.TryCatchAction(
            action: m => {
                id.CheckNotEmpty("id");
                if (GetCacheUser.Organize_Id != id)
                {
                    throw new Exception("id:尝试获取组织机构信息失败...");
                }
                var result = m.IQOrganizes.FirstOrDefault(a => a.Id == id).MapTo<OrganizeOutDto>();
                return new OperationResult(OperationResultType.Success, "获取数据成功！", result);
            }));
        #endregion

        #region 获取当前用户管理的组织机构
        /// <summary>
        /// 获取当前用户管理的组织机构
        /// </summary>
        /// <param name="grid">
        ///  FilterGroup:{"Rules": [],"Groups": [],"Operate": 1}
        ///  SortField:Id,CreatedTime
        ///  SortOrder:desc,asc
        ///  排序字段对应一种排序方式
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("Organize/GetOrganizeGridData")]
        [Description("获取当前用户管理的组织机构")]
        public IHttpActionResult GetOrganizeGridData([FromBody]GridRequestsModel grid) => Json(OrganizeService.TryCatchAction(
            action: m => {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;
                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception($"id:你的操作权限等级过低");
                }

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
                var queryable = (from a in m.IQUserOrganizeMap
                                 join b in m.IQOrganizes
                                 on a.Organize_Id equals b.Id
                                 where a.UserLogin_Id == cacheUser.Id
                                 select b).AsQueryable();

                if (cacheUser.IsAdministrator)
                {
                    queryable = m.IQOrganizes;
                }

                Expression<Func<Organize, bool>> predicate = FilterHelper.GetExpression<Organize>(request.FilterGroup);

                var data = queryable.ToPageCache<Organize, OrganizePageOutDto>(predicate, request.PageCondition);

                //把用户获取的组织机构信息主键缓存起来，留待修改时校正
                var cacheUserId = data.ListData.Select(b => b.Id);
                ICache iCache = CacheManager.GetCacher("CacheOrganizeId");
                iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));

                return new OperationResult(OperationResultType.Success, "获取数据成功", data);
            }));
        #endregion

        #region 添加新的组织机构
        /// <summary>
        /// 添加新的组织机构
        /// </summary>
        /// <param name="organizes">组织机构集合</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Organize/AddOrganizes")]
        [Description("添加新的组织机构")]
        public async Task<IHttpActionResult> AddOrganizes([FromBody] params OrganizeIn[] organizes) => Json(await OrganizeService.TryCatchActionAsync(
            action: async m =>
            {
                organizes.CheckNotNullOrEmpty("organizes");
                var cacheUser = GetCacheUser;
                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception($"id:你的操作权限等级过低");
                }
                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<Organize>();
                cache.Clear();
                // --------------------------------------------
                return await m.AddOrganizes(cacheUser, organizes);
            }));
        #endregion

        #region 编辑修改组织机构
        /// <summary>
        /// 编辑修改组织机构
        /// </summary>
        /// <param name="organizes">编辑的机构信息集合</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Organize/EditOrganizes")]
        [Description("编辑修改组织机构")]
        public async Task<IHttpActionResult> EditOrganizes([FromBody] params OrganizeInputDto[] organizes) => Json(await OrganizeService.TryCatchActionAsync(
            action: async m =>
            {
                organizes.CheckNotNullOrEmpty("organizes");
                var cacheUser = GetCacheUser;
                // 不管信息是否更改成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<Organize>();
                cache.Clear();
                // --------------------------------------------
                return await m.EditOrganizes(cacheUser, organizes);
            }));
        #endregion

        #region 删除指定的组织机构
        /// <summary>
        /// 删除指定的组织机构
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Organize/DeleteById")]
        [Description("删除指定的组织机构")]
        public async Task<IHttpActionResult> DeleteById([FromBody]params Guid[] ids) => Json(await OrganizeService.TryCatchActionAsync(action: async m =>
        {
            ids.CheckNotNullOrEmpty("ids");
            var cache = GetCacheUser;
            if (!cache.IsAdministrator)
            {
                if (!cache.Level.IsBetween(1, 2))
                {
                    throw new Exception("id:你没有权限删除组织机构！");
                }
                if (ids.Contains(GetCacheUser.Organize_Id))
                {
                    throw new Exception("id:删除目标集合中包含自身所属组织机构,操作已被终止！");
                }

                ICache cache1 = CacheManager.GetCacher("CacheOrganizeId");
                IEnumerable<Guid> cacheIds = cache1.Get(cache.UserName.AESEncrypt128()) as IEnumerable<Guid>;

                foreach (var i in ids)
                {
                    if (!ListOrganizeId.Contains(i))
                    {
                        throw new Exception($"id:你没有权限删除id={i}的组织机构！");
                    }
                    if (!cacheIds.Contains(i))
                    {
                        throw new Exception($"id:值主键{i}不在范围内,请尝试重新获取数据后在操作...");
                    }
                }
            }

            // 不管删除是否执行成功，都执行删除当前对象页的缓存
            ICache cache2 = CacheManager.GetCacher<Organize>();
            cache2.Clear();
            // -------------------------------------------------

            return await m.DeleteOrganizeById(ids);
        }));
        
        #endregion
    }
}
