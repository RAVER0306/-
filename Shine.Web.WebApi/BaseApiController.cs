using Shine.Comman.Logging;
using Shine.Web.WebApi.Filters;
using System;
using System.Web.Http;

namespace Shine.Web.WebApi
{
    /// <summary>
    /// WebAPI的控制器基类
    /// </summary>
    [OnAuthorize]
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// 日志记录对象
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseApiController()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// 获取或设置 依赖注入服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }
    }
}
