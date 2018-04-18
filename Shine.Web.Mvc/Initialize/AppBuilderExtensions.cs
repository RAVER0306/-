using Owin;
using Shine.Comman;
using Shine.Core;
using Shine.Core.Dependency;
using Shine.Web.Mvc.Binders;
using System.Web.Mvc;

namespace Shine.Web.Mvc.Initialize
{
    /// <summary>
    /// <see cref="IAppBuilder"/>初始化扩展
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 初始化Mvc框架
        /// </summary>
        public static IAppBuilder UseShineMvc(this IAppBuilder app, IIocBuilder iocBuilder)
        {
            iocBuilder.CheckNotNull("iocBuilder");

            ModelBinders.Binders.Add(typeof(string), new StringTrimModelBinder());

            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(iocBuilder);
            return app;
        }
    }
}