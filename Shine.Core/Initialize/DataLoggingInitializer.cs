using Shine.Core.Configs;

namespace Shine.Core.Initialize
{
    /// <summary>
    /// 数据日志初始化器，用于初始化数据日志功能
    /// </summary>
    public class DataLoggingInitializer : LoggingInitializerBase, IDataLoggingInitializer
    {
        /// <summary>
        /// 开始初始化数据日志
        /// </summary>
        /// <param name="config">数据日志配置信息</param>
        public virtual void Initialize(DataLoggingConfig config)
        {
            LoggingAdapterConfig adapterConfig = new LoggingAdapterConfig()
            {
                Enabled = config.Enabled,
                AdapterType = config.AdapterType
            };
            SetLoggingFromAdapterConfig(adapterConfig);
        }
    }
}