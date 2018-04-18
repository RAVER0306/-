using EntityFramework;
using EntityFramework.Batch;
using System.Data.Entity;

namespace Shine.Data.EF.MySql
{
    /// <summary>
    /// MySql数据上下文初始化
    /// </summary>
    public class MySqlDefaultDbContextInitializer : DbContextInitializerBase<DefaultDbContext>
    {
        /// <summary>
        /// 初始化一个<see cref="MySqlDefaultDbContextInitializer"/>新实例
        /// </summary>
        public MySqlDefaultDbContextInitializer()
        {
            CreateDatabaseInitializer = MigrateInitializer
                = new MigrateDatabaseToLatestVersion<DefaultDbContext, MySqlMigrationsConfiguration<DefaultDbContext>>();
            Locator.Current.Register<IBatchRunner>(() => new MySqlBatchRunner());
        }
    }
}