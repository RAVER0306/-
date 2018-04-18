using Shine.Comman;
using System;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Shine.Data.EF
{
    /// <summary>
    /// 上下文事务单元操作实现
    /// </summary>
    public class UnitOfWork : Disposable
    {
        #region Overrides of Disposable

        /// <summary>
        /// 重写以实现释放派生类资源的逻辑
        /// </summary>
        protected override void Disposing()
        {
            DbContext context = new DbContext("");
            DbContextTransaction trans1 = context.Database.BeginTransaction();
            DbContextTransaction trans3 = context.Database.CurrentTransaction;

            SqlTransaction trans2 = new SqlConnection().BeginTransaction();

            context.Database.UseTransaction(trans2);

            trans1.Commit();

            throw new NotImplementedException();
        }

        #endregion
    }
}
