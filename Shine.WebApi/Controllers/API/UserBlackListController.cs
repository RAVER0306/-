using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Data.Extensions;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;


namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 黑名单信息相关接口
    /// </summary>
    [Description("黑名单信息相关接口")]
    public class UserBlackListController : ThisBaseApiController
    {
        #region 获取黑名单列表信息
        /// <summary>
        /// 获取黑名单列表信息
        /// </summary>
        /// <param name="grid">获取数据的一种特定格式</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserBlackList/GetUserBlackLists")]
        [Description("获取黑名单列表")]
        public IHttpActionResult GetUserBlackLists([FromBody]GridRequestsModel grid) => Json(UserBlacklistService.TryCatchAction(
            action: m =>
             {
                 grid.CheckNotNull("grid");
                 var cacheUser = GetCacheUser;

                 //查询条件
                 GridRequests request = new GridRequests(grid);
                 //添加默认排序，只有排序未设置的情况下生效
                 request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                 var queryable1 = m.UBLQueryable.Where(a => a.UserLogin_Id == cacheUser.Id)
                                   .Select(b => new BlackListView
                                   {
                                       Id = b.Id,
                                       UserName = b.UserLoginOne.UserName,
                                       DataItemDetailName = b.DataItemDetailOne.FullName,
                                       DataItemDetailIndex = b.DataItemDetailOne.Index,
                                       BlackList = b.BlackList,
                                       CreatedTime = b.CreatedTime,
                                       FullName = b.FullName
                                   });
                 if (cacheUser.IsAdministrator)
                 {
                     queryable1 = m.UBLQueryable.Select(b => new BlackListView
                     {
                         Id = b.Id,
                         UserName = b.UserLoginOne.UserName,
                         DataItemDetailName = b.DataItemDetailOne.FullName,
                         DataItemDetailIndex = b.DataItemDetailOne.Index,
                         BlackList = b.BlackList,
                         CreatedTime = b.CreatedTime,
                         FullName = b.FullName
                     });
                 }

                 Expression<Func<BlackListView, bool>> predicate = FilterHelper.GetExpression<BlackListView>(request.FilterGroup);

                 var data = queryable1.ToPageCache<BlackListView, BlackListPageOutDto>(predicate, request.PageCondition);

                 //把用户获取的信息主键缓存起来，留待修改时校正
                 var cacheUserId = data.ListData.Select(b => b.Id);
                 ICache iCache = CacheManager.GetCacher("CacheUserBlackListPageId");
                 iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));

                 return new OperationResult(OperationResultType.Success, "获取数据成功", data);
             }));
        #endregion

        #region 编辑黑名单
        [HttpPost]
        [Route("UserBlackList/EditUserBlackLists")]
        [Description("编辑黑名单")]
        public IHttpActionResult EditUserBlackLists([FromBody] params UserBlackListInputDto[] datas) => Json(UserBlacklistService.TryCatchAction(
            action:m=>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;
                return m.EditUserBlackLists(cacheUser, datas);
            }));
        #endregion

        #region 添加黑名单(用户每种黑名单类型都只能创建一个)
        [HttpPost]
        [Route("UserBlackList/AddUserBlackLists")]
        [Description("添加黑名单")]
        public IHttpActionResult AddUserBlackLists([FromBody] params UserBlackListInputDto[] datas) => Json(UserBlacklistService.TryCatchAction(
            action: m => 
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;
                return m.AddUserBlackLists(cacheUser, datas);
            }));
        #endregion

        #region 删除黑名单
        [HttpPost]
        [Route("UserBlackList/DeleteUserBlackLists")]
        [Description("删除黑名单")]
        public IHttpActionResult DeleteUserBlackLists([FromBody] params Guid[] Ids) => Json(UserBlacklistService.TryCatchAction(
            action: m => 
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;
                return m.DeleteUserBlackLists(cacheUser, Ids);
            }));
        #endregion
    }
}
