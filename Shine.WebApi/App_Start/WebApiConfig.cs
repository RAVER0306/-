using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Shine.WebApi
{
    /// <summary>
    /// Web API 配置和服务
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Web API 配置和服务
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            // 默认路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
