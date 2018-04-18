using Shine.Core.Logging;
using System;

namespace Shine.Data.EF.Logging
{
    /// <summary>
    /// 数据日志映射配置
    /// </summary>
    public class DataLogConfiguration : EntityConfigurationBase<DataLog, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="DataLogConfiguration"/>类型的新实例
        /// </summary>
        public DataLogConfiguration()
        {
            this.HasOptional(m => m.OperateLog)
                .WithMany(n => n.DataLogs)
                .WillCascadeOnDelete(true);
        }

        /// <summary>
        /// 获取 相关上下文类型，如为null，将使用默认上下文，否则使用指定的上下文类型
        /// </summary>
        public override Type DbContextType
        {
            get { return typeof(LoggingDbContext); }
        }

    }
}