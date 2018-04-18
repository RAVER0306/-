using MySql.Data.Entity;
using Shine.Core.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Shine.Data.EF.MySql
{
    /// <summary>
    /// MySql自动迁移配置类
    /// </summary>
    /// <typeparam name="TContext">MySql 数据上下文</typeparam>
    public class MySqlMigrationsConfiguration<TContext> : DbMigrationsConfiguration<TContext>
        where TContext : DbContext, IUnitOfWork
    {
        private const string ProviderName = "MySql.Data.MySqlClient";

        /// <summary>
        /// 初始化一个<see cref="MySqlMigrationsConfiguration{TContext}"/>类型的新实例
        /// </summary>
        public MySqlMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(TContext).FullName;

            SetSqlGenerator(ProviderName, new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory(ProviderName, (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

    }
}