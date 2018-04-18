using Shine.DataProcessingLogic.Base.OrgizeManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.OrganizeManager
{
    /// <summary>
    /// 字典目录信息
    /// </summary>
    [Description("字典目录信息")]
    public class DataItem: DataItemBase<Guid>
    {
        public DataItem()
        {
            DataItemDetailMany = new HashSet<DataItemDetail>();
            Children = new HashSet<DataItem>();
        }

        /// <summary>
        /// 获取或设置 该信息的父级<see cref="DataItem"/>实体对象主键
        /// </summary>
        public new Guid? ParentId { get; set; }

        /// <summary>
        /// 获取或设置 该信息的父级<see cref="DataItem"/>实体对象
        /// </summary>
        public DataItem Parent { get; set; }

        /// <summary>
        /// 获取或设置 子级<see cref="DataItem"/>信息集合
        /// </summary>
        public virtual ICollection<DataItem> Children { get; set; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 实体集合
        /// </summary>
        public virtual ICollection<DataItemDetail> DataItemDetailMany { set; get; }
    }
}
