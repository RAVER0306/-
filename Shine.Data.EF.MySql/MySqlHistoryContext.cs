using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;


namespace Shine.Data.EF.MySql
{
    /// <summary>
    /// MySql迁移历史上下文，更改<see cref="HistoryRow"/>模型以适应MySql数据库的特性
    /// site:http://stackoverflow.com/questions/20832546/entity-framework-with-mysql-and-migrations-failing-because-max-key-length-is-76
    /// </summary>
    public class MySqlHistoryContext : HistoryContext
    {
        /// <summary>
        /// 初始化一个<see cref="MySqlHistoryContext"/>类型的新实例
        /// </summary>
        public MySqlHistoryContext(DbConnection existingConnection, string defaultSchema)
            : base(existingConnection, defaultSchema)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryRow>().Property(m => m.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(m => m.ContextKey).HasMaxLength(200).IsRequired();
        }
    }
}