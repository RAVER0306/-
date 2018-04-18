using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.WebApi.Utils
{
    /// <summary>
    /// Grid查询请求
    /// </summary>
    public class GridRequests
    {
        /// <summary>
        /// 初始化一个<see cref="GridRequests"/>类型的新实例
        /// </summary>
        public GridRequests(GridRequestsModel request)
        {
            string jsonWhere = request.FilterGroup;
            FilterGroup = !jsonWhere.IsNullOrEmpty() ? JsonHelper.FromJson<FilterGroup>(jsonWhere) : new FilterGroup();

            int pageIndex = request.PageIndex.CastTo(1);
            int pageSize = request.PageSize.CastTo(25);
            PageCondition = new PageCondition(pageIndex, pageSize);
            string sortField = request.SortField;
            string sortOrder = request.SortOrder;
            if (!sortField.IsNullOrEmpty() && !sortOrder.IsNullOrEmpty())
            {
                string[] fields = sortField.Split(",", true);
                string[] orders = sortOrder.Split(",", true);
                if (fields.Length != orders.Length)
                {
                    throw new ArgumentException("查询列表的排序参数个数不一致。");
                }
                List<SortCondition> sortConditions = new List<SortCondition>();
                for (int i = 0; i < fields.Length; i++)
                {
                    ListSortDirection direction = orders[i].ToLower() == "desc"
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
                    sortConditions.Add(new SortCondition(fields[i], direction));
                }
                PageCondition.SortConditions = sortConditions.ToArray();
            }
            else
            {
                PageCondition.SortConditions = new SortCondition[] { };
            }
        }

        /// <summary>
        /// 获取 查询条件组
        /// </summary>
        public FilterGroup FilterGroup { get; private set; }

        /// <summary>
        /// 获取 分页查询条件信息
        /// </summary>
        public PageCondition PageCondition { get; private set; }

        /// <summary>
        /// 添加默认排序条件，只有排序条件为空时有效
        /// </summary>
        /// <param name="conditions"></param>
        public void AddDefaultSortCondition(params SortCondition[] conditions)
        {
            if (PageCondition.SortConditions.Length == 0)
            {
                PageCondition.SortConditions = conditions;
            }
        }
    }    
}