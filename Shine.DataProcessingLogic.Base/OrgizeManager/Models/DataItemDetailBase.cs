using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Base.OrgizeManager.Models
{
    /// <summary>
    /// 字典详细信息基类
    /// </summary>
    /// <typeparam name="TKey">字典详细信息主键类型</typeparam>
    public abstract class DataItemDetailBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime,
        ILockable
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 字典目录名称
        /// </summary>
        [StringLength(64)]
        public string FullName { set; get; }

        /// <summary>
        /// 获取或设置 字典目录的查询编码
        /// </summary>
        [StringLength(64)]
        public string QueryCoding { set; get; }

        /// <summary>
        /// 获取或设置 该数据是否被锁定(冻结)
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 该数据是否公共数据
        /// </summary>
        public bool IsPublic { set; get; }

        /// <summary>
        /// 获取或设置 该信息的描述
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 该数据的序号(用作多语言判断)
        /// </summary>
        [Range(0,255)]
        public byte Index{ set; get; }

        /// <summary>
        /// 获取或设置是否系统预设
        /// </summary>
        public bool IsSystem { set; get; }
    }
}
