using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Data.Extensions;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 主机基本信息管理接口
    /// </summary>
    [Description("用户帐号相关信息管理接口")]
    public class HostController : ThisBaseApiController
    {
        #region 获取用户所管理的组织机构下的主机信息
        /// <summary>
        /// 获取用户所管理的组织机构下的主机信息
        /// </summary>
        /// <param name="grid">
        ///  FilterGroup:{"Rules": [],"Groups": [],"Operate": 1}
        ///  SortField:id,CreatedTime
        ///  SortOrder:desc,asc
        ///  排序字段对应一种排序方式
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("Host/GetHostGridData")]
        [Description("获取用户所管理的组织机构下的主机信息")]
        public IHttpActionResult GetHostGridData([FromBody]GridRequestsModel grid) => Json(HostService.TryCatchAction(action: m =>
        {
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
            //查询用户所管理的组织机构下的主机信息
            var queryable = MyHostqueryable;

            //如果超级管理员查询全部的主机信息
            if (cacheUser.IsAdministrator)
            {
                queryable = m.HostQueryable;
            }
            Expression<Func<Host, bool>> predicate = FilterHelper.GetExpression<Host>(request.FilterGroup);

            var data = queryable.ToPageCache<Host, HostPageOutDto>(predicate, request.PageCondition);

            //缓存用户获取的主机列表主机，用于操作验证
            var cacheUserId = data.ListData.Select(b => b.Id);
            ICache iCache = CacheManager.GetCacher("CacheHostId");
            iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));
            //-------------------------------------------------------------------------------------

            return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
        }));
        #endregion

        #region 获取指定主机信息
        [HttpPost]
        [Route("Host/FindHostOne/{id}")]
        [Description("获取指定主机信息")]
        public IHttpActionResult FindHostOne(Guid id) => Json(HostService.TryCatchAction(
          action:m =>
          {
              id.CheckNotEmpty("id");
              var cacheUser = GetCacheUser;

              Host queryable = m.HostQueryable.FirstOrDefault(mx => mx.Id == id && ListOrganizeId.Contains(mx.Organize_Id));
              if (cacheUser.IsAdministrator)
              {
                  queryable = m.HostQueryable.FirstOrDefault(mx => mx.Id == id);
              }
              HostPageOutDto host = queryable.MapTo<HostPageOutDto>();
              return new OperationResult(OperationResultType.Success,"请求数据成功",host) ;
          }));       
        #endregion

        #region 添加新的主机
        /// <summary>
        /// 添加主机信息
        /// </summary>
        /// <param name="hosts">主机信息集合</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Host/AddHosts")]
        [Description("添加新的主机")]
        public IHttpActionResult AddHosts([FromBody]params HostInputDto[] hosts) => Json(HostService.TryCatchAction(
            action: m =>
            {
                hosts.CheckNotNullOrEmpty("hosts");
                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<Host>();
                cache.Clear();
                // --------------------------------------------
                return m.AddHosts(GetCacheUser, ListOrganizeId.ToArray(), hosts);
            }));


        #endregion

        #region 更新主机基本信息
        /// <summary>
        /// 更新主机基本信息
        /// </summary>
        /// <param name="hosts">主机信息集合</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Host/EditHosts")]
        [Description("添加新的主机")]
        public IHttpActionResult EditHosts([FromBody]params HostInputDto[] hosts) => Json(HostService.TryCatchAction(
            action: m =>
            {
                hosts.CheckNotNullOrEmpty("hosts");

                var cacheUser = GetCacheUser;

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<Host>();
                cache.Clear();
                // --------------------------------------------

                ICache iCache = CacheManager.GetCacher("CacheHostId");
                Guid[] cachePageId = (iCache.Get(cacheUser.UserName.AESEncrypt128()) as IEnumerable<Guid>).ToArray();
                return m.EditHosts(cachePageId, hosts);
            }));
        #endregion

        #region 删除主机基本信息
        /// <summary>
        /// 删除主机的基本信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Host/DeleteHosts")]
        [Description("删除主机基本信息")]
        public IHttpActionResult DeleteHosts([FromBody] params Guid[] Ids) => Json(HostService.TryCatchAction(
            action: m =>
            {
                Ids.CheckNotNullOrEmpty("hosts");

                var cacheUser = GetCacheUser;

                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception("id:你没有权限进行此项操作！");
                }

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<Host>();
                cache.Clear();
                // --------------------------------------------

                ICache iCache = CacheManager.GetCacher("CacheHostId");
                Guid[] cachePageId = (iCache.Get(cacheUser.UserName.AESEncrypt128()) as IEnumerable<Guid>).ToArray();
                return m.DeleteHosts(cachePageId, Ids);
            }));
        #endregion

        #region 获取主机HostLogin表的数据信息
        /// <summary>
        ///  获取主机HostLogin表的数据信息
        /// </summary>
        /// <param name="grid">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Host/GetHostLogins")]
        [Description("获取主机HostLogin表的数据信息")]
        public IHttpActionResult GetHostLogins([FromBody]GridRequestsModel grid) => Json(HostService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;//获取缓存判断

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("UpdateTime", ListSortDirection.Descending));
                //查询用户所管理的组织机构下的主机信息
                var queryable1 = MyHostqueryable;
                //如果超级管理员查询全部的主机信息
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = m.HostQueryable;
                }

                //查询管理主机关联的主机登陆参数
                var queryable2 = from c in m.HostLoginQueryable
                                 join d in queryable1
                                 on c.Host_Id equals d.Id
                                 select c;

                Expression<Func<HostLogin, bool>> predicate = FilterHelper.GetExpression<HostLogin>(request.FilterGroup);

                var data = queryable2.ToPage<HostLogin, HostLoginPageOutDto>(predicate, request.PageCondition);
                return new OperationResult(OperationResultType.Success, "操作成功", data);
            }));
        #endregion

        #region 获取主机HostParameter参数数据
        /// <summary>
        /// 获取主机HostParameter参数数据
        /// </summary>
        /// <param name="grid">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Host/GetHostParameters")]
        [Description("获取主机HostParameter表的数据信息")]
        public IHttpActionResult GetHostParameter([FromBody]GridRequestsModel grid) => Json(HostService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;//获取缓存判断

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("UpdateTime", ListSortDirection.Descending));
                //查询用户所管理的组织机构下的主机信息
                var queryable1 = MyHostqueryable;
                //如果超级管理员查询全部的主机信息
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = m.HostQueryable;
                }

                //查询管理主机关联的主机登陆参数
                var queryable2 = from c in m.HostParameterQueryable
                                 join d in queryable1
                                 on c.Host_Id equals d.Id
                                 select c;

                Expression<Func<HostParameter, bool>> predicate = FilterHelper.GetExpression<HostParameter>(request.FilterGroup);

                var data = queryable2.ToPage<HostParameter, HostParameterPageOutDto>(predicate, request.PageCondition);
                return new OperationResult(OperationResultType.Success, "操作成功", data);
            }));
        #endregion

        #region 获取主机实时数据
        /// <summary>
        /// 获取主机实时数据
        /// </summary>
        /// <param name="grid">查询参数集合</param>
        /// <returns></returns>
        [HttpPost]
        [Description("获取主机实时数据")]
        [Route("Host/GetRealTimeDataHosts")]
        public IHttpActionResult GetRealTimeDataHosts([FromBody]GridRequestsModel grid) => Json(HostService.TryCatchAction(
            action: m =>
             {
                 grid.CheckNotNull("grid");
                 var cacheUser = GetCacheUser;
                 if (!cacheUser.Level.IsBetween(1, 2))
                 {
                     throw new Exception($"id:你的操作权限等级过低");
                 }

                 //查询条件
                 GridRequests request = new GridRequests(grid);
                 //添加默认排序，只有排序未设置的情况下生效
                 request.AddDefaultSortCondition(new SortCondition("UpdateTime", ListSortDirection.Descending));
                 //查询用户所管理的组织机构下的主机信息
                 var queryable = from a in m.HostRealTimeDataHostQueryable
                                 join b in MyHostqueryable
                                 on a.Host_Id equals b.Id
                                 select new HostReadTimeDataView
                                 {
                                     Id = a.Id,
                                     Host_Id = a.Host_Id,
                                     Current = a.Current,
                                     HostName = a.HostOne.FullName,
                                     IsOnline = a.IsOnline,
                                     Latitude = a.Latitude,
                                     Longitude = a.Longitude,
                                     LoopState = a.LoopState,
                                     OranizeName = a.HostOne.OrganizeOne.FullName,
                                     Organize_Id = a.HostOne.Organize_Id,
                                     Power = a.Power,
                                     Temperature = a.Temperature,
                                     UpdateTime = a.UpdateTime,
                                     Voltage = a.Voltage,
                                     TimeZone=a.TimeZone,
                                 };

                 //如果超级管理员查询全部的主机信息
                 if (cacheUser.IsAdministrator)
                 {
                     queryable = from a in m.HostRealTimeDataHostQueryable
                                 select new HostReadTimeDataView
                                 {
                                     Id = a.Id,
                                     Host_Id = a.Host_Id,
                                     Current = a.Current,
                                     HostName = a.HostOne.FullName,
                                     IsOnline = a.IsOnline,
                                     Latitude = a.Latitude,
                                     Longitude = a.Longitude,
                                     LoopState = a.LoopState,
                                     OranizeName = a.HostOne.OrganizeOne.FullName,
                                     Organize_Id = a.HostOne.Organize_Id,
                                     Power = a.Power,
                                     Temperature = a.Temperature,
                                     UpdateTime = a.UpdateTime,
                                     Voltage = a.Voltage,
                                     TimeZone = a.TimeZone,
                                 }; 
                 }

                 Expression<Func<HostReadTimeDataView, bool>> predicate = FilterHelper.GetExpression<HostReadTimeDataView>(request.FilterGroup);

                 var data = queryable.ToPage<HostReadTimeDataView, HTDPageOutDto>(predicate, request.PageCondition);
                 return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
             }));
        #endregion

        #region 获取指定组织机构下的主机数量
        [HttpPost]
        [Description("获取指定组织机构下的主机数量")]
        [Route("Host/GetHostNum")]
        public IHttpActionResult GetHostNum([FromBody] params Guid[] Ids) =>Json(HostService.TryCatchAction(
            action: m => {
                var cacheUser = GetCacheUser;
                if (!Ids.IsEmpty())
                {
                    if (!cacheUser.IsAdministrator)
                    {
                        foreach (var id in Ids)
                        {
                            if (!ListOrganizeId.Contains(id))
                            {
                                throw new Exception($"id:没有权限查询组织机构:{id}");
                            }
                        }
                    }
                    return m.GetHostCount(Ids);
                }
                else
                {
                    return m.GetHostCount(ListOrganizeId.ToArray());
                }
            }));
        #endregion
    }
}
