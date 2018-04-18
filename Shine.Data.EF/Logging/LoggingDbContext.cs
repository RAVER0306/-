namespace Shine.Data.EF.Logging
{
    /// <summary>
    /// 日志数据上下文
    /// </summary>
    public class LoggingDbContext : DbContextBase<LoggingDbContext>
    {
        /// <summary>
        /// 获取 是否允许数据库日志记录
        /// </summary>
        protected override bool DataLoggingEnabled { get { return false; } }

    }
}