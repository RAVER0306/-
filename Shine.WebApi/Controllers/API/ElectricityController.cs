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
    /// 能耗统计相关接口
    /// </summary>
    [Description("能耗统计相关接口")]
    public class ElectricityController : ThisBaseApiController
    {
        #region 查询光照计划
        /// <summary>
        /// 查询光照计划
        /// </summary>
        /// <returns></returns>
        //[Description("查询光照计划")]
        //[HttpPost]
        //[Route("LightPlan/GetLightPlans")]
        //public IHttpActionResult GetLightPlans() => Json(LightPlanService.TryCatchAction(
        //    action: m =>
        //    {

        //    }));
        #endregion
    }
}
