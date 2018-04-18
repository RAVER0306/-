using Microsoft.Owin.Security.OAuth;
using Owin;
using Shine.Comman;
using Shine.Core;
using Shine.Core.Dependency;
using Shine.Web.WebApi.Filters;
using Shine.Web.WebApi.Handlers;
using Shine.Web.WebApi.Selectors;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Shine.Web.WebApi.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化WebApi框架
        /// </summary>
        public static IAppBuilder UseShineWebApi(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }

        /// <summary>
        /// 初始化WebApi
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IAppBuilder ConfigureWebApi(this IAppBuilder app)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // 注册请求生命周期Scope的处理器
            config.MessageHandlers.Add(new RequestLifetimeScopeHandler());
            // API自定义错误消息处理委托类。
            // 用于处理访问不到对应API地址的情况，对错误进行自定义操作。
            config.MessageHandlers.Add(new CustomErrorMessageDelegatingHandler());

            //全局异常处理
            //config.Filters.Add(new ExceptionHandlingAttribute());
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonMediaTypeFormatter());

            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //config.EnsureInitialized();
            return app;
        }
    }
}