using Shine.Core.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Shine.Data.EF.Migrations
{
    /// <summary>
    /// 自动迁移配置
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class AutoMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext>
        where TContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// 初始化一个<see cref="AutoMigrationsConfiguration{TContext}"/>类型的新实例
        /// </summary>
        public AutoMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            //是否可以自动迁移，注意这可能会导致数据库数据丢失
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(TContext).FullName;
        }
    }
}
