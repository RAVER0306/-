using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
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
    /// 光照计划信息管理接口
    /// </summary>
    [Description("光照计划信息管理接口")]
    public class LightPlanController : ThisBaseApiController
    {
        #region 添加光照计划
        /// <summary>
        /// 添加光照计划
        /// </summary>
        /// <returns></returns>
        [Description("添加光照计划")]
        [HttpPost]
        [Route("LightPlan/AddLightPlans")]
        public IHttpActionResult AddLightPlans([FromBody] params LightPlanInputDto[] datas) => Json(LightPlanService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;

                // 不管信息是否执行成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<LightPlanView>();
                cache.Clear();
                // --------------------------------------------
                return m.AddLightPlans(cacheUser, datas);
            }));
        #endregion

        #region 编辑光照计划
        /// <summary>
        /// 编辑光照计划
        /// </summary>
        /// <returns></returns>
        [Description("编辑光照计划")]
        [HttpPost]
        [Route("LightPlan/EditLightPlans")]
        public IHttpActionResult EditLightPlans([FromBody] params LightPlanInputDto[] datas) => Json(LightPlanService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;

                // 不管信息是否执行成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<LightPlanView>();
                cache.Clear();
                // --------------------------------------------
                return m.EditLightPlans(cacheUser, datas);
            }));
        #endregion

        #region 删除光照计划
        /// <summary>
        /// 删除光照计划
        /// </summary>
        /// <returns></returns>
        [Description("删除光照计划")]
        [HttpPost]
        [Route("LightPlan/DeleteLightPlans")]
        public IHttpActionResult DeleteLightPlans([FromBody] params Guid[] Ids) => Json(LightPlanService.TryCatchAction(
            action: m =>
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;

                // 不管信息是否执行成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<LightPlanView>();
                cache.Clear();
                // --------------------------------------------

                return m.DeleteLightPlans(cacheUser, Ids);
            }));
        #endregion

        #region 查询光照计划
        /// <summary>
        /// 查询光照计划
        /// </summary>
        /// <returns></returns>
        [Description("查询光照计划")]
        [HttpPost]
        [Route("LightPlan/GetLightPlans")]
        public IHttpActionResult GetLightPlans([FromBody]GridRequestsModel grid) => Json(LightPlanService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                var queryable1 = from a in m.LightPlanQueryable
                                 join b in MyHostqueryable
                                 on a.Host_Id equals b.Id
                                 select new LightPlanView
                                 {
                                     Id = a.Id,
                                     Host_Id = a.Host_Id,
                                     CreatedTime = a.CreatedTime,
                                     DataItemDetailName = a.DataItemDetailOne.FullName,
                                     DataItemDetail_Id = a.DataItemDetail_Id,
                                     HostName = a.HostOne.FullName,
                                     OrganizeName = a.HostOne.OrganizeOne.FullName,
                                     Organize_Id = a.HostOne.Organize_Id,
                                     AutoSwitch = a.AutoSwitch,
                                     Brightness = a.Brightness,
                                     CurrentBrightness = a.CurrentBrightness,
                                     Enable = a.Enable,
                                     GroupTexs = a.GroupTexs,
                                     GroupSwitch = a.GroupSwitch,
                                     IlluminationCurve = a.IlluminationCurve,
                                     LoopState = a.LoopState,
                                     MaxBrightness = a.MaxBrightness,
                                     MaxLimit = a.MaxLimit,
                                     MinBrightness = a.MinBrightness,
                                     MinLimit = a.MinLimit,
                                     ScaleFactor = a.ScaleFactor,
                                     PlanID = a.PlanID,
                                     TriggerTime = a.TriggerTime,
                                     UpdatedTime = a.UpdatedTime
                                 };
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = from a in m.LightPlanQueryable
                                 join b in HostService.HostQueryable
                                 on a.Host_Id equals b.Id
                                 select new LightPlanView
                                 {
                                     Id = a.Id,
                                     Host_Id = a.Host_Id,
                                     CreatedTime = a.CreatedTime,
                                     DataItemDetailName = a.DataItemDetailOne.FullName,
                                     DataItemDetail_Id = a.DataItemDetail_Id,
                                     HostName = a.HostOne.FullName,
                                     OrganizeName = a.HostOne.OrganizeOne.FullName,
                                     Organize_Id = a.HostOne.Organize_Id,
                                     AutoSwitch = a.AutoSwitch,
                                     Brightness = a.Brightness,
                                     CurrentBrightness = a.CurrentBrightness,
                                     Enable = a.Enable,
                                     GroupTexs = a.GroupTexs,
                                     GroupSwitch = a.GroupSwitch,
                                     IlluminationCurve = a.IlluminationCurve,
                                     LoopState = a.LoopState,
                                     MaxBrightness = a.MaxBrightness,
                                     MaxLimit = a.MaxLimit,
                                     MinBrightness = a.MinBrightness,
                                     MinLimit = a.MinLimit,
                                     ScaleFactor = a.ScaleFactor,
                                     PlanID = a.PlanID,
                                     TriggerTime = a.TriggerTime,
                                     UpdatedTime = a.UpdatedTime
                                 };
                }

                Expression<Func<LightPlanView, bool>> predicate = FilterHelper.GetExpression<LightPlanView>(request.FilterGroup);

                var data = queryable1.ToPageCache<LightPlanView, LightPlanPageOutDto>(predicate, request.PageCondition);

                //缓存用户获取的主机列表主机，用于操作验证
                var cacheUserId = data.ListData.Select(b => b.Id);
                ICache iCache = CacheManager.GetCacher("CachePlanId");
                iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));
                //-------------------------------------------------------------------------------------

                return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
            }));
        #endregion
    }
}
