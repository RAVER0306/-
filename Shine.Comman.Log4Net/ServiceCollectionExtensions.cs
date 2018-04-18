using Shine.Core.Configs;
using Shine.Core.Dependency;
using Shine.Core.Initialize;

namespace Shine.Comman.Log4Net
{
    /// <summary>
    /// 服务映射信息集合扩展操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Log4Net日志功能相关映射信息
        /// </summary>
        public static void AddLog4NetServices(this IServiceCollection services)
        {
            if (ShineConfig.LoggingConfigReseter == null)
            {
                ShineConfig.LoggingConfigReseter = new Log4NetLoggingConfigReseter();
            }
            services.AddSingleton<IBasicLoggingInitializer, Log4NetLoggingInitializer>();
            services.AddSingleton<Log4NetLoggerAdapter>();
        }
    }
}