using Shine.Core.Dependency;

namespace Shine.Core.Logging
{
    /// <summary>
    /// 操作日志输出接口
    /// </summary>
    public interface IOperateLogWriter : IScopeDependency
    {
        /// <summary>
        /// 输出操作日志
        /// </summary>
        /// <param name="operateLog">操作日志信息</param>
        void Write(OperateLog operateLog);
    }
}