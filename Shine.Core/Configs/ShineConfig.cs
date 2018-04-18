using Shine.Core.Configs.ConfigFile;
using System;
using System.Configuration;

namespace Shine.Core.Configs
{
    /// <summary>
    /// 配置类
    /// </summary>
    public sealed class ShineConfig
    {
        private const string CoreSectionName = "shine";
        private static readonly Lazy<ShineConfig> InstanceLazy
            = new Lazy<ShineConfig>(() => new ShineConfig());

        /// <summary>
        /// 初始化一个新的<see cref="CoreConfig"/>实例
        /// </summary>
        private ShineConfig()
        {
            ShineFrameworkSection section = (ShineFrameworkSection)ConfigurationManager.GetSection(CoreSectionName);
            if (section == null)
            {
                DataConfig = new DataConfig();
                LoggingConfig = new LoggingConfig();
                return;
            }
            DataConfig = new DataConfig(section.Data);
            LoggingConfig = new LoggingConfig(section.Logging);
        }

        /// <summary>
        /// 获取 配置类的单一实例
        /// </summary>
        public static ShineConfig Instance
        {
            get
            {
                ShineConfig config = InstanceLazy.Value;
                if (DataConfigReseter != null)
                {
                    config.DataConfig = DataConfigReseter.Reset(config.DataConfig);
                }
                if (LoggingConfigReseter != null)
                {
                    config.LoggingConfig = LoggingConfigReseter.Reset(config.LoggingConfig);
                }
                return config;
            }
        }

        /// <summary>
        /// 获取或设置 数据配置重置信息
        /// </summary>
        public static IDataConfigReseter DataConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 日志配置重置信息
        /// </summary>
        public static ILoggingConfigReseter LoggingConfigReseter { get; set; }

        /// <summary>
        /// 获取或设置 数据配置信息
        /// </summary>
        public DataConfig DataConfig { get; set; }

        /// <summary>
        /// 获取或设置 日志配置信息
        /// </summary>
        public LoggingConfig LoggingConfig { get; set; }
    }
}