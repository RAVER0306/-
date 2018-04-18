using Shine.Data.EF.Logging;
using System.Data.Entity;

namespace Shine.Data.EF.MySql
{
    /// <summary>
    /// MySql的log数据库上下文初始化
    /// </summary>
    public class MySqlLoggingDbContextInitializer : DbContextInitializerBase<LoggingDbContext>
    {
        /// <summary>
        /// 初始化一个<see cref="MySqlLoggingDbContextInitializer"/>新实例
        /// </summary>
        public MySqlLoggingDbContextInitializer()
        {
            CreateDatabaseInitializer = MigrateInitializer
                = new MigrateDatabaseToLatestVersion<LoggingDbContext, MySqlMigrationsConfiguration<LoggingDbContext>>();
        }
    }
}