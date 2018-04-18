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
    /// 字典目录详细信息
    /// </summary>
    [Description("字典目录详细信息")]
    public class DataItemDetail: DataItemDetailBase<Guid>
    {
        /// <summary>
        /// 获取或设置 <see cref="DataItem"/> 实体对象主键
        /// </summary>
        public Guid DataItem_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItem"/> 实体对象
        /// </summary>
        public DataItem DataItemOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid? Organzie_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }
    }
}
