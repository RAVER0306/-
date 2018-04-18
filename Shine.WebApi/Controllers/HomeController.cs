using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Caching.Models;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Models.UserManager;
using Shine.Web.Mvc;
using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace Shine.WebApi.Controllers
{
    /// <summary>
    /// 初始化主页
    /// </summary>
    [Description("主页控制器")]
    public class HomeController :BaseController
    {     
        /// <summary>
        /// 网页初始化
        /// </summary>
        /// <returns></returns>
        [Description("网站-首页")]
        public ActionResult Index()
        {
            ICache iCache = CacheManager.GetCacher<CacheUser>();
            CacheUser userLogin = iCache.Get("admin".AESEncrypt128()) as CacheUser ?? new CacheUser();
            ViewBag.User = userLogin.UserName;
            return View();
        }
    }
}