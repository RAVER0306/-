using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
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
    /// 分控信息管理接口
    /// </summary>
    [Description("分控信息管理接口")]
    public class SubControlController : ThisBaseApiController
    {
        #region 添加分控基本信息
        /// <summary>
        /// 添加分控基本信息
        /// </summary>
        /// <returns></returns>
        [Description("添加分控基本信息")]
        [HttpPost]
        [Route("SubControl/AddSubControls")]
        public IHttpActionResult AddSubControls([FromBody] params SubControlInputDto[] datas) => Json(SubControlService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<SubControlView>();
                cache.Clear();
                // --------------------------------------------

                return m.AddSubControls(cacheUser, datas);
            }));
        #endregion

        #region 获取指定分控信息
        [HttpPost]
        [Route("Host/FindSubOne/{id}")]
        [Description("获取指定分控信息")]
        public IHttpActionResult FindSubOne(Guid id) => Json(SubControlService.TryCatchAction(
          action: m =>
          {
              id.CheckNotEmpty("id");
              var cacheUser = GetCacheUser;

              SubControl queryable = m.SubControlQueryable.FirstOrDefault(mx => mx.Id == id && ListOrganizeId.Contains(mx.LigthPoleOne.HostOne.Organize_Id));
              if (cacheUser.IsAdministrator)
              {
                  queryable = m.SubControlQueryable.FirstOrDefault(mx => mx.Id == id);
              }
              SubControlView subv = queryable.MapTo<SubControlView>();
              return new OperationResult(OperationResultType.Success, "请求数据成功", subv);
          }));
        #endregion

        #region 编辑分控
        /// <summary>
        /// 编辑分控
        /// </summary>
        /// <returns></returns>
        [Description("编辑分控")]
        [HttpPost]
        [Route("SubControl/EditSubControls")]
        public IHttpActionResult EditSubControls([FromBody] params SubControlInputDto[] datas) => Json(SubControlService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<SubControlView>();
                cache.Clear();
                // --------------------------------------------

                return m.EditSubControls(cacheUser, datas);
            }));
        #endregion

        #region 删除分控
        /// <summary>
        /// 删除分控
        /// </summary>
        /// <returns></returns>
        [Description("删除分控")]
        [HttpPost]
        [Route("SubControl/DeleteSubControls")]
        public IHttpActionResult DeleteSubControls([FromBody] params Guid[] Ids) => Json(SubControlService.TryCatchAction(
            action: m =>
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<SubControlView>();
                cache.Clear();
                // --------------------------------------------

                return m.DeleteSubControls(cacheUser,Ids);
            }));
        #endregion

        #region 查询分控
        /// <summary>
        /// 查询分控
        /// </summary>
        /// <returns></returns>
        [Description("查询分控")]
        [HttpPost]
        [Route("SubControl/GetSubControls")]
        public IHttpActionResult GetSubControls([FromBody]GridRequestsModel grid) => Json(SubControlService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                var queryable1 = from a in LightPoleService.LightPoleQueryable
                                 join b in MyHostqueryable
                                 on a.Host_Id equals b.Id
                                 select new LightPoleView
                                 {
                                     Address = a.Address,
                                     Id = a.Id,
                                     Host_Id = a.Host_Id,
                                     CreatedTime = a.CreatedTime,
                                     DataItemDetailName = a.DataItemDetailOne.FullName,
                                     DataItemDetail_Id = a.DataItemDetail_Id,
                                     HostName = a.HostOne.FullName,
                                     PoleName = a.PoleName,
                                     PoleNum = a.PoleNum,
                                     Remark = a.Remark,
                                     OrganizeName = a.HostOne.OrganizeOne.FullName,
                                     Organize_Id = a.HostOne.Organize_Id,
                                     RegPacket = a.HostOne.RegPackage
                                 };

                var queryable2 = from s in m.SubControlQueryable
                                 join l in queryable1
                                 on s.LigthPoleOne_Id equals l.Id
                                 select new SubControlView
                                 {
                                     Id = s.Id,
                                     DataItemDetail_Id = s.DataItemDetail_Id,
                                     LigthPoleOne_Id = s.LigthPoleOne_Id,
                                     CreatedTime = s.CreatedTime,
                                     DataItemDetailName = s.DataItemDetailOne.FullName,
                                     LigthPoleOneName = s.LigthPoleOne.PoleName,
                                     OrganizeName = l.OrganizeName,
                                     Organize_Id = l.Organize_Id,
                                     Remark = s.Remark,
                                     SubName = s.SubName,
                                     SubNum = s.SubNum,
                                     UID = s.UID,
                                     RegPacket = l.RegPacket,
                                     Latitude=s.Latitude,
                                     Longitude=s.Longitude
                                 };
                if (cacheUser.IsAdministrator)
                {
                    queryable2 = from a in m.SubControlQueryable
                                 join b in LightPoleService.LightPoleQueryable
                                 on a.LigthPoleOne_Id equals b.Id
                                 select new SubControlView
                                 {
                                     Id = a.Id,
                                     DataItemDetail_Id = a.DataItemDetail_Id,
                                     LigthPoleOne_Id = a.LigthPoleOne_Id,
                                     CreatedTime = a.CreatedTime,
                                     DataItemDetailName = a.DataItemDetailOne.FullName,
                                     LigthPoleOneName = a.LigthPoleOne.PoleName,
                                     OrganizeName = b.HostOne.OrganizeOne.FullName,
                                     Organize_Id = b.HostOne.Organize_Id,
                                     Remark = a.Remark,
                                     SubName = a.SubName,
                                     SubNum = a.SubNum,
                                     UID = a.UID,
                                     RegPacket = b.HostOne.RegPackage,
                                     Latitude = a.Latitude,
                                     Longitude = a.Longitude
                                 };

                }

                Expression<Func<SubControlView, bool>> predicate = FilterHelper.GetExpression<SubControlView>(request.FilterGroup);

                var data = queryable2.ToPageCache<SubControlView, SubControlPageOutDto>(predicate, request.PageCondition);

                //缓存用户获取的主机列表主机，用于操作验证
                var cacheUserId = data.ListData.Select(b => b.Id);
                ICache iCache = CacheManager.GetCacher("CacheSubControlsId");
                iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));
                //-------------------------------------------------------------------------------------

                return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
            }));
        #endregion

        #region 查询分控上灯具的实时数据
        [Description("查询分控实时数据")]
        [HttpPost]
        [Route("SubControl/GetSubReadTimeDatas")]
        public IHttpActionResult GetSubReadTimeDatas([FromBody]GridRequestsModel grid) => Json(SubControlService.TryCatchAction(
           action: m =>
           {
               grid.CheckNotNull("grid");
               var cacheUser = GetCacheUser;

               //查询条件
               GridRequests request = new GridRequests(grid);
               //添加默认排序，只有排序未设置的情况下生效
               request.AddDefaultSortCondition(new SortCondition("UpdateTime", ListSortDirection.Descending));

               var queryable1 = from a in m.SubAggregationQueryable
                                join b in ListOrganizeId
                                on a.Organzie_Id equals b
                                join c in m.SubControlQueryable
                                on a.SubControl_Id equals c.Id
                                join d in m.SubRealTimeDataQueryable
                                on c.Id equals d.SubControl_Id
                                select new SubReadTimeDataView
                                {
                                    Brightness = d.Brightness,
                                    SubControl_Id = d.SubControl_Id,
                                    Id = d.Id,
                                    CreatedTime = d.CreatedTime,
                                    Current = d.Current,
                                    DataItemDetailName = d.DataItemDetailOne.FullName,
                                    FullName = d.FullName,
                                    DataItemDetail_Id = d.DataItemDetail_Id,
                                    DimmingPort = d.DimmingPort,
                                    Frequency = d.Frequency,
                                    Power = d.Power,
                                    SubControlName = d.SubControlOne.SubName,
                                    Temperature = d.Temperature,
                                    UpdateTime = d.UpdateTime,
                                    Voltage = d.Voltage,
                                    RegPacket =d.SubControlOne.LigthPoleOne.HostOne.RegPackage
                                };


               if (cacheUser.IsAdministrator)
               {
                   queryable1 = from d in m.SubRealTimeDataQueryable
                                select new SubReadTimeDataView
                                {
                                    Brightness = d.Brightness,
                                    SubControl_Id = d.SubControl_Id,
                                    Id = d.Id,
                                    CreatedTime = d.CreatedTime,
                                    Current = d.Current,
                                    DataItemDetailName = d.DataItemDetailOne.FullName,
                                    FullName = d.FullName,
                                    DataItemDetail_Id = d.DataItemDetail_Id,
                                    DimmingPort = d.DimmingPort,
                                    Frequency = d.Frequency,
                                    Power = d.Power,
                                    SubControlName = d.SubControlOne.SubName,
                                    Temperature = d.Temperature,
                                    UpdateTime = d.UpdateTime,
                                    Voltage = d.Voltage,
                                    RegPacket = d.SubControlOne.LigthPoleOne.HostOne.RegPackage
                                };
               }

               Expression<Func<SubReadTimeDataView, bool>> predicate = FilterHelper.GetExpression<SubReadTimeDataView>(request.FilterGroup);

               var data = queryable1.ToPageCache<SubReadTimeDataView,SRTDPageOutDto>(predicate, request.PageCondition);

               return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
           }));
        #endregion

        #region 添加分控上的灯具基本信息
        [Description("添加分控上的灯具基本信息")]
        [HttpPost]
        [Route("SubControl/AddSubReadTimeDatas")]
        public IHttpActionResult AddSubReadTimeDatas([FromBody]params SubRealTimeDataInputDto[] datas) => Json(SubControlService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;
                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<SubReadTimeDataView>();
                cache.Clear();
                // --------------------------------------------
                return m.AddSubReadTimeDatas(cacheUser, datas);

            }));
        #endregion

        #region 编辑分控上的灯具基本信息 
        [Description("编辑分控上的灯具基本信息 ")]
        [HttpPost]
        [Route("SubControl/EditSubReadTimeDatas")]
        public IHttpActionResult EditSubReadTimeDatas([FromBody]params SubRealTimeDataInputDto[] datas) => Json(SubControlService.TryCatchAction(
            action: m => 
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;
                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<SubReadTimeDataView>();
                cache.Clear();
                // --------------------------------------------
                return m.EditSubReadTimeDatas(cacheUser, datas);
            }));
        #endregion

        #region 删除分控上的灯具基本信息
        [HttpPost]
        [Description("删除分控上的灯具基本信息")]
        [Route("SubControl/DeleteSubReadTimeDatas")]
        public IHttpActionResult DeleteSubReadTimeDatas([FromBody]Guid[] Ids) => Json(SubControlService.TryCatchAction(
            action: m => 
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;
                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<SubReadTimeDataView>();
                cache.Clear();
                // --------------------------------------------
                return m.DeleteReadTimeDatas(cacheUser, Ids);
            }));
        #endregion

        #region 更新分控经纬度
        [Route("SubControl/EidtSubLocation")]
        [HttpPost]
        [Description("更新分控经纬度")]
        public IHttpActionResult EidtSubLocation(Guid id, double x1, double y2) => Json(SubControlService.TryCatchAction(
            action: m => 
            {
                id.CheckNotEmpty("id");
                var cacheUser = GetCacheUser;
                return m.EidtSubLocation(id, x1, y2);
            }));

        #endregion

        #region 获取指定组织机构下的分控数量
        [HttpPost]
        [Description("获取指定组织机构下的分控数量")]
        [Route("SubControl/GetSubNum")]
        public IHttpActionResult GetSubNum([FromBody]params Guid[] Ids) => Json(SubControlService.TryCatchAction(
           action: m => {
               if (!Ids.IsEmpty())
               {
                   var user = GetCacheUser;
                   if (!GetCacheUser.IsAdministrator)
                   {
                       foreach (Guid id in Ids)
                       {
                           if (!ListOrganizeId.Contains(id))
                           {
                               throw new Exception($"id:没有权限查询组织机构信息:{id}");
                           }
                       }
                   }
                   return m.GetSubCount(Ids);
               }
               else
               {
                   return m.GetSubCount(ListOrganizeId.ToArray());
               }
           }));
        #endregion

        #region 获取指定组织机构的灯具数量
        [HttpPost]
        [Description("获取指定组织机构的灯具数量")]
        [Route("SubControl/GetLightNum")]
        public IHttpActionResult GetLightNum([FromBody]params Guid[] Ids) => Json(SubControlService.TryCatchAction(
            action:m=>
            {
                if (!Ids.IsEmpty())
                {
                    var user = GetCacheUser;
                    if (!GetCacheUser.IsAdministrator)
                    {
                        foreach (Guid id in Ids)
                        {
                            if (!ListOrganizeId.Contains(id))
                            {
                                throw new Exception($"id:没有权限查询组织机构信息:{id}");
                            }
                        }
                    }
                    return m.GetLightCount(Ids);
                }
                else
                {
                    return m.GetLightCount(ListOrganizeId.ToArray());
                }
            }));
        #endregion
    }
}
