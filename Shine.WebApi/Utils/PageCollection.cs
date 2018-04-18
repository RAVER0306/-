using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Core.Data;
using Shine.Core.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Shine.WebApi.Utils
{
    /// <summary>
    /// 分页集合查询帮助
    /// </summary>
    public static class PageCollection
    {
        internal static IQueryable<TEntity> GetQueryData<TEntity, TKey>(this IQueryable<TEntity> source, out int total, GridRequests request)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            Expression<Func<TEntity, bool>> predicate = FilterHelper.GetExpression<TEntity>(request.FilterGroup);
            return source.Where(predicate, request.PageCondition, out total);
        }

        internal static PageResult<TResult> GetPageResult<TEntity, TResult>(this IQueryable<TEntity> source, Expression<Func<TEntity, TResult>> selector, GridRequests request)
        {
            Expression<Func<TEntity, bool>> predicate = FilterHelper.GetExpression<TEntity>(request.FilterGroup);
            return source.ToPage(predicate, request.PageCondition, selector);
        }

        internal static PageResult<TResult> GetPageResult<TEntity, TResult>(this IQueryable<TEntity> source, Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, GridRequests request, bool IsAdministrator = false)
        {
            //FilterGroup filter = new FilterGroup();
            //filter.Rules.AddUpgradeFile(new FilterRule("IsAdministrator", false));
            //Expression<Func<TEntity, bool>> predicates = IsAdministrator ? FilterHelper.GetExpression<TEntity>(filter) : predicate.And(FilterHelper.GetExpression<TEntity>(request.FilterGroup));
            return source.ToPage(predicate, request.PageCondition, selector);
        }
    }
}