using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Data.Extensions;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 主机策略相关信息管理接口
    /// </summary>
    [Description("主机策略相关信息管理接口")]
    public class HostPolicyController : ThisBaseApiController
    {
        #region 获取管理的主机下的所有策略
        /// <summary>
        /// 获取管理的主机下的所有策略
        /// </summary>
        /// <param name="grid">数据参数</param>
        /// <returns></returns>
        [Description("获取管理的主机下的所有策略")]
        [HttpPost]
        [Route("HostPolicy/GetHostPolicys")]
        public IHttpActionResult GetHostPolicys([FromBody]GridRequestsModel grid) => Json(HostPolicyService.TryCatchAction(
            action: m =>
             {
                 grid.CheckNotNull("grid");
                 var cacheUser = GetCacheUser;//获取缓存判断

                 //查询条件
                 GridRequests request = new GridRequests(grid);
                 //添加默认排序，只有排序未设置的情况下生效
                 request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                 //查询用户所管理的组织机构下的主机信息
                 var queryable1 = MyHostqueryable;
                 //如果超级管理员查询全部信息
                 if (cacheUser.IsAdministrator)
                 {
                     queryable1 = HostService.HostQueryable;
                 }

                 //查询管理主机关联的主机登陆参数
                 var queryable2 = from c in m.HostPolicyQueryable
                                  join d in queryable1
                                  on c.Host_Id equals d.Id
                                  select c;

                 Expression<Func<HostPolicy, bool>> predicate = FilterHelper.GetExpression<HostPolicy>(request.FilterGroup);

                 var data = queryable2.ToPageCache<HostPolicy, HostPolicyPageOutDto>(predicate, request.PageCondition);

                 //缓存用户获取的主机列表主机，用于操作验证
                 var cacheUserId = data.ListData.Select(b => b.Id);
                 ICache iCache = CacheManager.GetCacher("CacheHostPolicyId");
                 iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));
                 //-------------------------------------------------------------------------------------
                 return new OperationResult(OperationResultType.Success, "操作成功", data);
             }));
        #endregion

        #region 添加主机策略信息
        /// <summary>
        /// 添加主机策略信息
        /// </summary>
        /// <param name="datas">待添加的主机策略集合</param>
        /// <returns></returns>
        [Description("添加主机的策略信息")]
        [HttpPost]
        [Route("HostPolicy/AddHostPolicys")]
        public IHttpActionResult AddHostPolicys([FromBody]params HostPolicyInputDto[] datas) => Json(HostPolicyService.TryCatchAction(
            action: m =>
             {
                 datas.CheckNotNullOrEmpty("datas");
                 var cacheUser = GetCacheUser;//获取缓存判断

                 // 不管信息是否添加成功？都执行删除当前对象页的缓存
                 ICache cache = CacheManager.GetCacher<HostPolicy>();
                 cache.Clear();
                 // --------------------------------------------

                 return m.AddHostPolicys(cacheUser, datas);
             }));
        #endregion

        #region 更新主机策略信息
        /// <summary>
        /// 更新主机策略信息
        /// </summary>
        /// <param name="datas">待更新的主机策略集合</param>
        /// <returns></returns>
        [Description("更新主机策略信息")]
        [HttpPost]
        [Route("HostPolicy/EditHostPolicys")]
        public IHttpActionResult EditHostPolicys([FromBody]params HostPolicyInputDto[] datas) => Json(HostPolicyService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;//获取缓存判断

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<HostPolicy>();
                cache.Clear();
                // --------------------------------------------
                ICache iCache = CacheManager.GetCacher("CacheHostPolicyId");
                var cachePageIds = (iCache.Get(cacheUser.UserName.AESEncrypt128()) as IEnumerable<Guid>).ToArray();
                return m.EditHostPolicys(cachePageIds, cacheUser, datas);
            }));
        #endregion

        #region 删除策略信息
        /// <summary>
        /// 删除主机策略信息
        /// </summary>
        /// <param name="ids">待删除的主机策略ID集合</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除主机策略信息")]
        [Route("HostPolicy/DeleteHostpolicys")]
        public IHttpActionResult DeleteHostpolicys([FromBody]params Guid[] ids) => Json(HostPolicyService.TryCatchAction(
            action: m => 
            {
                ids.CheckNotNullOrEmpty("ids");
                var cacheUser = GetCacheUser;//获取缓存判断

                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception("id:你没有权限进行此项操作！");
                }

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<HostPolicy>();
                cache.Clear();
                // --------------------------------------------

                ICache iCache = CacheManager.GetCacher("CacheHostPolicyId");
                var cachePageIds = (iCache.Get(cacheUser.UserName.AESEncrypt128()) as IEnumerable<Guid>).ToArray();
                if (cachePageIds.Length==0)
                {
                    throw new Exception($"id:未能找到准备删除的缓存数据！");
                }
                return m.DeleteHostpolicys(cachePageIds,ids);
            }));
        #endregion

        #region 获取主机策略下发日志
        /// <summary>
        /// 获取主机策略下发日志
        /// </summary>
        /// <param name="grid">查询规则数据集</param>
        /// <returns></returns>
        [HttpPost]
        [Route("HostPolicy/GetHostpolicyLogs")]
        [Description("获取主机策略下发日志")]
        public IHttpActionResult GetHostpolicyLogs([FromBody]GridRequestsModel grid) => Json(HostPolicyService.TryCatchAction(
            action: m =>
             {
                 grid.CheckNotNull("grid");
                 var cacheUser = GetCacheUser;//获取缓存判断

                 //查询条件
                 GridRequests request = new GridRequests(grid);
                 //添加默认排序，只有排序未设置的情况下生效
                 request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
                 var queryable1 = from a in m.HostPolicyLogQueryable
                                  join b in ListOrganizeId
                                  on a.Organzie_Id equals b
                                  select (new HostPolicyLogView
                                  {
                                      CreatedTime = a.CreatedTime,
                                      DataItemDetailIndex = a.DataItemDetailOne.Index,
                                      DataItemDetailName = a.DataItemDetailOne.FullName,
                                      HostName = a.HostOne.FullName,
                                      HostRegPacket = a.HostOne.RegPackage,
                                      HostPolicyName = a.HostPolicyOne.FullName,
                                      HostPolicyNum = a.HostPolicyOne.Number,
                                      Id = a.Id,
                                      OrganizeName = a.OrganizeOne.FullName,
                                      UserName = a.UserLoginOne.UserName
                                  });
                 //如果超级管理员查询全部信息
                 if (cacheUser.IsAdministrator)
                 {
                     queryable1 = m.HostPolicyLogQueryable.Select(a => new HostPolicyLogView
                     {
                         CreatedTime = a.CreatedTime,
                         DataItemDetailIndex = a.DataItemDetailOne.Index,
                         DataItemDetailName = a.DataItemDetailOne.FullName,
                         HostName = a.HostOne.FullName,
                         HostRegPacket = a.HostOne.RegPackage,
                         HostPolicyName = a.HostPolicyOne.FullName,
                         HostPolicyNum = a.HostPolicyOne.Number,
                         Id = a.Id,
                         OrganizeName = a.OrganizeOne.FullName,
                         UserName = a.UserLoginOne.UserName
                     });
                 }

                 Expression<Func<HostPolicyLogView, bool>> predicate = FilterHelper.GetExpression<HostPolicyLogView>(request.FilterGroup);

                 var data = queryable1.ToPage<HostPolicyLogView,HostPolicyLogOutDto>(predicate, request.PageCondition);
                 return new OperationResult(OperationResultType.Success, "操作成功", data);
             }));
        #endregion

        #region 删除日志下载日志
        /// <summary>
        /// 删除日志下载日志
        /// </summary>
        /// <param name="Ids">准备要删除的日志主键集合</param>
        /// <returns></returns>
        [HttpPost]
        [Route("HostPolicy/DeleteHostPolicyLogs")]
        [Description("删除日志下载日志")]
        public IHttpActionResult DeleteHostPolicyLogs([FromBody]Guid[] Ids) => Json(HostPolicyService.TryCatchAction(
            action: m => 
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;
                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception("id:你没有权限进行此项操作！");
                }
                return m.DeleteHostPolicyLogs(Ids);
            }));
        #endregion
    }
}
