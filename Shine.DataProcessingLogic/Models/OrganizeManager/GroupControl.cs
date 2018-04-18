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
    /// 分组信息设置表实体
    /// </summary>
    [Description("分组信息设置表实体")]
    public class GroupControl: GroupControlBase<Guid>
    {
        /// <summary>
        /// 获取或设置 分组对象主键
        /// </summary>
        public Guid? ObjectId { set; get; }


        /// <summary>
        /// 获取或设置 关联的 <see cref="DataItemDetail"/> 类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 关联的 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organzie_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }
    }
}
