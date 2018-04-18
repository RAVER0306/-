using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.UserManager.Models
{
    /// <summary>
    /// 用户设置关联黑名单基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class UserBlackListBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 黑名单的名称
        /// </summary>
        [StringLength(64)]
        public string FullName { set; get; }

        /// <summary>
        /// 获取或设置 黑名单内容
        /// </summary>
        [MaxLength]
        public string BlackList { set; get; }

        /// <summary>
        /// 获取或设置 创建该数据的时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
