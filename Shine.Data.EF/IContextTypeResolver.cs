using Shine.Core.Data;
using Shine.Core.Dependency;
using System;

namespace Shine.Data.EF
{
    /// <summary>
    /// 定义数据上下文实例创建器
    /// </summary>
    public interface IDbContextTypeResolver : ISingletonDependency
    {
        /// <summary>
        /// 由实体类型获取关联的上下文类型
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns></returns>
        IUnitOfWork Resolve<TEntity, TKey>()
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// 由实体类型获取关联的上下文类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        IUnitOfWork Resolve(Type entityType);
    }
}