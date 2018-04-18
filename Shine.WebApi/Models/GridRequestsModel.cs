using Shine.Comman.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    /// <summary>
    /// Grid查询请求实体
    /// </summary>
    public class GridRequestsModel
    {
        /// <summary>
        /// 条件字段查询组
        /// </summary>
        public string FilterGroup { set; get; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { set; get; }

        /// <summary>
        /// 页大小
        /// </summary>

        public int PageSize { set; get; }

        /// <summary>
        /// 要排序的字段
        /// </summary>
        public string SortField { set; get; }

        /// <summary>
        /// 排序的方式
        /// </summary>
        public string SortOrder { set; get; }
    }
}