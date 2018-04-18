using Shine.Comman.AutoMapper;
using Shine.Comman.Log4Net;
using Shine.Comman.Logging;
using Shine.Core;
using Shine.Core.Dependency;
using Shine.Data.EF;
using Shine.Web.Mvc.Autofac.Mvc;
using Shine.Web.WebApi.Autofac.Http;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shine.WebApi
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class Global : HttpApplication
    {
        private ILogger _logger;
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 应用错误日志处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            GetLogger();
            Exception ex = Server.GetLastError();
            _logger.Fatal("全局异常Application_Error", ex);
        }

        private void GetLogger()
        {
            if (_logger == null)
            {
                _logger = LogManager.GetLogger<Global>();
            }
        }
    }
}