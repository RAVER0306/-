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
using Shine.DataProcessingLogic.Dtos.HostManager.In;
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
    /// <summary>
    /// 灯杆信息管理接口
    /// </summary>
    [Description("灯杆信息管理接口")]
    public class LightPoleController : ThisBaseApiController
    {
        #region 添加灯杆
        /// <summary>
        /// 添加灯杆
        /// </summary>
        /// <returns></returns>
        [Description("添加灯杆")]
        [HttpPost]
        [Route("LightPole/AddLightPoles")]
        public IHttpActionResult AddLightPoles([FromBody] params LightPoleInputDto[] datas) => Json(LightPoleService.TryCatchAction(
            action: m =>
             {
                 datas.CheckNotNullOrEmpty("datas");
                 var cacheUser = GetCacheUser;

                 // 不管信息是否添加成功？都执行删除当前对象页的缓存
                 ICache cache = CacheManager.GetCacher<LightPoleView>();
                 cache.Clear();
                 // --------------------------------------------
                 return m.AddLightPoles(cacheUser, datas);
             }));
        #endregion

        #region 编辑灯杆
        /// <summary>
        /// 编辑灯杆
        /// </summary>
        /// <returns></returns>
        [Description("编辑灯杆")]
        [HttpPost]
        [Route("LightPole/EditLightPoles")]
        public IHttpActionResult EditLightPoles([FromBody] params LightPoleInputDto[] datas) => Json(LightPoleService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<LightPoleView>();
                cache.Clear();
                // --------------------------------------------
                return m.EditLightPoles(cacheUser, datas);
            }));
        #endregion

        #region 删除灯杆
        /// <summary>
        /// 删除灯杆
        /// </summary>
        /// <returns></returns>
        [Description("删除灯杆")]
        [HttpPost]
        [Route("LightPole/DeleteLightPoles")]
        public IHttpActionResult DeleteLightPoles([FromBody] params Guid[]Ids) => Json(LightPoleService.TryCatchAction(
            action: m =>
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;

                // 不管信息是否添加成功？都执行删除当前对象页的缓存
                ICache cache = CacheManager.GetCacher<LightPoleView>();
                cache.Clear();
                // --------------------------------------------

                return m.DeleteLightPoles(cacheUser, Ids);
            }));
        #endregion

        #region 查询灯杆
        /// <summary>
        /// 查询灯杆
        /// </summary>
        /// <returns></returns>
        [Description("查询灯杆")]
        [HttpPost]
        [Route("LightPole/GetLightPoles")]
        public IHttpActionResult GetLightPoles([FromBody]GridRequestsModel grid) => Json(LightPoleService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                var queryable1 = from a in m.LightPoleQueryable
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
                                     RegPacket =b.RegPackage
                                 };
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = m.LightPoleQueryable.Select(a => new LightPoleView
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
                        RegPacket=a.HostOne.RegPackage
                    });
                }

                Expression<Func<LightPoleView, bool>> predicate = FilterHelper.GetExpression<LightPoleView>(request.FilterGroup);

                var data = queryable1.ToPageCache<LightPoleView, LightPolePageOutDto>(predicate, request.PageCondition);

                //缓存用户获取的主机列表主机，用于操作验证
                var cacheUserId = data.ListData.Select(b => b.Id);
                ICache iCache = CacheManager.GetCacher("CachePoleId");
                iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));
                //-------------------------------------------------------------------------------------

                return new OperationResult(OperationResultType.Success, "获取数据成功！", data);
            }));
        #endregion
    }
}
