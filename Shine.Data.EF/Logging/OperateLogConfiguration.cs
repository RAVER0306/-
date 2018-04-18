using Shine.Core.Logging;
using System;

namespace Shine.Data.EF.Logging
{
    /// <summary>
    /// 操作日志映射配置
    /// </summary>
    public class OperateLogConfiguration : EntityConfigurationBase<OperateLog, Guid>
    {
        /// <summary>
        /// 获取 相关上下文类型，如为null，将使用默认上下文，否则使用指定的上下文类型
        /// </summary>
        public override Type DbContextType
        {
            get { return typeof(LoggingDbContext); }
        }
    }
}