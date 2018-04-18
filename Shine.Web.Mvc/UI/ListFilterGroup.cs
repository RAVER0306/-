using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using System.Web;

namespace Shine.Web.Mvc.UI
{
    /// <summary>
    /// 列表数据筛选条件组
    /// </summary>
    public class ListFilterGroup : FilterGroup
    {
        /// <summary>
        /// 初始化一个<see cref="ListFilterGroup"/>类型的新实例
        /// </summary>
        public ListFilterGroup(HttpRequestBase request)
        {
            string jsonWhere = request.Params["filter_group"];
            if (jsonWhere.IsNullOrEmpty())
            {
                return;
            }
            FilterGroup group = JsonHelper.FromJson<FilterGroup>(jsonWhere);
            Rules = group.Rules;
            Groups = group.Groups;
            Operate = group.Operate;
        }
    }
}
