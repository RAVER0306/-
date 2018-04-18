using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.UserManager.Models
{
    /// <summary>
    /// 用户基本信息基类
    /// </summary>
    /// <typeparam name="TKey">用户基本信息主键类型</typeparam>
    public abstract class UserBase<TKey> :
        EntityBase<TKey>, 
        ICreatedAudited, 
        IUpdateAudited
        where TKey : IEquatable<TKey>
    {

        /// <summary>
        /// 获取或设置 用户真实姓名
        /// </summary>
        [StringLength(128)]
        public string RealName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [StringLength(128)]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 用户头像流
        /// </summary>
        public byte[] HeadIcon { set; get; }

        /// <summary>
        /// 获取或设置 用户头像存储的地址
        /// </summary>
        [StringLength(512)]
        public string HeadIconPath { set; get; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        [StringLength(128),EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱是否验证
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 用户绑定的微信
        /// </summary>
        [StringLength(128)]
        public string WeChat { set; get; }

        /// <summary>
        /// 获取或设置 用户绑定微信是否验证
        /// </summary>
        public bool WeChatConfirmed { set; get; }

        /// <summary>
        /// 获取或设置 手机号码
        /// </summary>
        [StringLength(32),Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置 手机号码是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 用户信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 用户性别
        /// 说明：Sex = 0未定义性别，
        ///       Sex = 1定义性别男，
        ///       Sex = 2定义性别女
        /// </summary>
        [DefaultValue(0), Range(0, 2)]
        public byte Sex { get; set; }

        /// <summary>
        /// 获取或设置 用户是否接收系统信息
        /// </summary>
        [DefaultValue(true)]
        public bool IsSysReceive { get; set; }

        /// <summary>
        /// 获取或设置 用户设置的系统语言
        /// </summary>
        [StringLength(256)]
        public string Language { get; set; }

        /// <summary>
        /// 获取或设置 用户设置的系统语言
        /// </summary>
        [StringLength(256)]
        public string Theme { get; set; }

        /// <summary>
        /// 获取或设置 是否接收故障警报信息
        /// </summary>
        [DefaultValue(true)]
        public bool IsAlarm { get; set; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 创建该信息的用户ID
        /// </summary>
        [StringLength(128)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 获取或设置 信息最后被修改的时间
        /// </summary>
        [DefaultValue("1900-01-01")]
        public DateTime? LastUpdatedTime { set; get; }

        /// <summary>
        /// 获取或设置 修改该信息的用户ID
        /// </summary>
        [StringLength(128)]
        public string LastUpdatorUserId { set; get; }
    }
}
