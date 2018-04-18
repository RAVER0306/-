using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Base.OrgizeManager.Models
{
    /// <summary>
    /// 分组信息设置表实体基类
    /// </summary>
    /// <typeparam name="TKey">分组信息设置表实体主键类型</typeparam>
    public abstract class GroupControlBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 分组编号
        /// </summary>
        public int GrounpNum { set; get; }

        /// <summary>
        /// 获取或设置分组名称
        /// </summary>
        public string GrounpName { set; get; }

        /// <summary>
        /// 获取或设置分组内容
        /// 存储形式用","隔开
        /// 如:1,1,1,1,1,1
        /// </summary>
        [MaxLength]
        public string GroupContent { set; get; }

        /// <summary>
        /// 获取或设置 该信息的说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// 获取或设置 分组信息更新的时间
        /// </summary>

        public DateTime UpdateTime { set; get; }
    }
}
