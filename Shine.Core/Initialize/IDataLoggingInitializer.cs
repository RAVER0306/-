using Shine.Core.Configs;

namespace Shine.Core.Initialize
{
    /// <summary>
    /// 定义数据日志初始化器，用于初始化数据日志功能
    /// </summary>
    public interface IDataLoggingInitializer
    {
        /// <summary>
        /// 开始初始化数据日志
        /// </summary>
        /// <param name="config">数据日志配置信息</param>
        void Initialize(DataLoggingConfig config);
    }
}