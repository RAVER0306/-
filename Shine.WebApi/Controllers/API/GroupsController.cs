using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core;
using Shine.Core.Caching;
using Shine.Core.Data.Extensions;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Dtos.UserManager;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace Shine.WebApi.Controllers.API
{
    [Description("分组配置")]
    public class GroupsController : ThisBaseApiController
    {
        #region 添加分组信息
        /// <summary>
        /// 添加分组信息
        /// </summary>
        /// <returns></returns>
        [Description("添加分组信息")]
        [HttpPost]
        [Route("Groups/AddGroups")]
        public IHttpActionResult AddGroups([FromBody]params GroupControlInputDto[] datas) => Json(GroupControlService.TryCatchAction(
            action: m =>
            {
                // 不管信息是否执行成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<GroupView>();
                cache.Clear();
                // --------------------------------------------
                var cacheUser = GetCacheUser;
                return m.AddGroupControls(cacheUser, datas);
            }));
        #endregion

        #region 编辑分组信息
        /// <summary>
        /// 编辑分组信息
        /// </summary>
        /// <returns></returns>
        [Description("编辑分组信息")]
        [HttpPost]
        [Route("Groups/EditGroups")]
        public IHttpActionResult EditGroups([FromBody]params GroupControlInputDto[] datas) => Json(GroupControlService.TryCatchAction(
            action: m =>
            {
                // 不管信息是否执行成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<GroupView>();
                cache.Clear();
                // --------------------------------------------

                var cacheUser = GetCacheUser;
                return m.EditGroupControls(cacheUser, datas);
            }));
        #endregion

        #region 删除分组信息
        /// <summary>
        /// 删除分组信息
        /// </summary>
        /// <returns></returns>
        [Description("删除分组信息")]
        [HttpPost]
        [Route("Groups/DeleteGroups")]
        public IHttpActionResult DeleteGroups([FromBody] Guid[] Ids) => Json(GroupControlService.TryCatchAction(
            action: m =>
            {
                // 不管信息是否执行成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<GroupView>();
                cache.Clear();
                // --------------------------------------------

                var cacheUser = GetCacheUser;
                return m.DeleteGroupControls(cacheUser, Ids);
            }));
        #endregion

        #region 查询分组信息
        /// <summary>
        /// 查询分组信息
        /// </summary>
        /// <returns></returns>
        [Description("查询分组信息")]
        [HttpPost]
        [Route("Groups/GetGroups")]
        public IHttpActionResult GetGroups([FromBody]GridRequestsModel grid) => Json(GroupControlService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                var queryable1 = from a in m.GroupControlQueryable
                                 join b in ListOrganizeId
                                 on a.Organzie_Id equals b
                                 select new GroupView
                                 {
                                     Id = a.Id,
                                     CreatedTime = a.CreatedTime,
                                     DataItemDetailName = a.DataItemDetailOne.FullName,
                                     DataItemDetail_Id = a.DataItemDetail_Id,
                                     OrganizeName = a.OrganizeOne.FullName,
                                     Organize_Id = a.Organzie_Id,
                                     GrounpName = a.GrounpName,
                                     GrounpNum = a.GrounpNum,
                                     GroupContent = a.GroupContent,
                                     Remark = a.Remark,
                                     UpdateTime = a.UpdateTime,
                                     ObjectId = a.ObjectId
                                 };
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = from a in m.GroupControlQueryable
                                 select new GroupView
                                 {
                                     Id = a.Id,
                                     CreatedTime = a.CreatedTime,
                                     DataItemDetailName = a.DataItemDetailOne.FullName,
                                     DataItemDetail_Id = a.DataItemDetail_Id,
                                     OrganizeName = a.OrganizeOne.FullName,
                                     Organize_Id = a.Organzie_Id,
                                     GrounpName = a.GrounpName,
                                     GrounpNum = a.GrounpNum,
                                     GroupContent = a.GroupContent,
                                     Remark = a.Remark,
                                     UpdateTime = a.UpdateTime,
                                     ObjectId = a.ObjectId
                                 };
                }

                Expression<Func<GroupView, bool>> predicate = FilterHelper.GetExpression<GroupView>(request.FilterGroup);

                var data = queryable1.ToPageCache<GroupView, GroupPageOutDto>(predicate, request.PageCondition);

                //缓存用户获取的主机列表主机，用于操作验证
                var cacheUserId = data.ListData.Select(b => b.Id);
                ICache iCache = CacheManager.GetCacher("CacheGroupId");
                iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));
                //-------------------------------------------------------------------------------------

                return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
            }));
        #endregion
    }
}
