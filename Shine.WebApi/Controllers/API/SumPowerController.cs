using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 能耗信息管理接口
    /// </summary>
    [Description("能耗信息管理接口")]
    public class SumPowerController : ThisBaseApiController
    {
        /// <summary>
        /// 按月能耗统计计算
        /// </summary>
        /// <returns></returns>
        [Description("按月能耗统计计算")]
        [HttpPost]
        [Route("SumPower/SumMonthPower")]
        public IHttpActionResult SumMonthPower(int Year, Guid DataItemDetailId,Guid OrganizeId) => Json(Sum_PowerService.TryCatchAction(
           action: m =>
           {
               var cacheUser = GetCacheUser;
               if (!cacheUser.IsAdministrator && !ListOrganizeId.Contains(OrganizeId))
               {
                   throw new Exception($"id:没有权限操作该组织机构");
               }
               return m.Sum_MonthPower(DataItemDetailId, OrganizeId, Year);
           }));

        /// <summary>
        /// 按天能耗统计计算
        /// </summary>
        /// <returns></returns>
        [Description("按月能耗统计计算")]
        [HttpPost]
        [Route("SumPower/SumDayPower")]
        public IHttpActionResult SumDayPower(int Year,int Month, Guid DataItemDetailId, Guid OrganizeId) => Json(Sum_PowerService.TryCatchAction(
           action: m =>
           {
               var cacheUser = GetCacheUser;
               if (!cacheUser.IsAdministrator && !ListOrganizeId.Contains(OrganizeId))
               {
                   throw new Exception($"id:没有权限操作该组织机构");
               }
               return m.Sum_DayPower(DataItemDetailId, OrganizeId,Year, Month);
           }));

        /// <summary>
        /// 按小时能耗统计计算
        /// </summary>
        /// <returns></returns>
        [Description("按小时能耗统计计算")]
        [HttpPost]
        [Route("SumPower/SumHourPower")]
        public IHttpActionResult SumHourPower(int Year, int Month,int Day, Guid DataItemDetailId, Guid OrganizeId) => Json(Sum_PowerService.TryCatchAction(
           action: m =>
           {
               var cacheUser = GetCacheUser;
               if (!cacheUser.IsAdministrator && !ListOrganizeId.Contains(OrganizeId))
               {
                   throw new Exception($"id:没有权限操作该组织机构");
               }
               return m.Sum_HourPower(DataItemDetailId, OrganizeId, Year, Month,Day);
           }));
    }
}
