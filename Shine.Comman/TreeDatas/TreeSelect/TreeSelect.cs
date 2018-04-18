using Shine.Comman.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Shine.Comman.TreeDatas
{
    public static class TreeSelect
    {
        public static string TreeSelectJson(this List<TreeSelectModel> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(TreeSelectJson(data, "0", ""));
            sb.Append("]");
            return sb.ToString();
        }
        private static string TreeSelectJson(List<TreeSelectModel> data, string parentId, string blank)
        {
            StringBuilder sb = new StringBuilder();
            var ChildNodeList = data.FindAll(t => t.ParentId == parentId);
            var tabline = "";
            if (parentId != "0")
            {
                tabline = "　　";
            }
            if (ChildNodeList.Count > 0)
            {
                tabline = tabline + blank;
            }
            foreach (TreeSelectModel entity in ChildNodeList)
            {
                entity.Text = tabline + entity.Text;
                string strJson = entity.ToJsonString();
                sb.Append(strJson);
                sb.Append(TreeSelectJson(data, entity.Id, tabline));
            }
            return sb.ToString().Replace("}{", "},{");
        }
    }
}