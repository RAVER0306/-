using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.WebApi.Core.Models
{
    [Description("用户基本信息")]
    public class User : EntityBase<Guid>, ICreatedAudited, IUpdateAudited
    {
        #region 属性

        /// <summary> 
        /// 用户姓名 
        /// </summary> 
        [StringLength(64)]
        public string RealName { set; get; }

        /// <summary> 
        /// 用户昵称 
        /// </summary> 
        [StringLength(64)]
        public string NickName { set; get; }

        /// <summary> 
        /// 用户头像位置 
        /// </summary> 
        [StringLength(512)]
        public string HeadIcon { set; get; }

        /// <summary> 
        /// 用户邮箱地址 
        /// </summary> 
        [StringLength(64)]
        public string Email { set; get; }

        /// <summary>
        /// 电子邮箱是否验证
        /// </summary>
        [DefaultValue(false)]
        public bool EmailConfirmed { get; set; }

        /// <summary> 
        /// 用户微信号码 
        /// </summary> 
        [StringLength(64)]
        public string WeChat { set; get; }

        /// <summary>
        /// 用户微信号码是否已经验证
        /// </summary>
        [DefaultValue(false)]
        public bool WeChatConfirmed { set; get; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(64)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号码是否验证
        /// </summary>
        [DefaultValue(false)]
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary> 
        /// 用户信息备注 
        /// </summary> 
        [StringLength(512)]
        public string Description { set; get; }

        /// <summary> 
        /// 用户性别：0未知 1男 2女 
        /// </summary> 
        [Range(0, 2)]
        [DefaultValue(0)]
        public byte Sex { set; get; }

        /// <summary> 
        /// 是否接收系统信息 
        /// </summary> 
        [DefaultValue(false)]
        public bool IsSysReceive { set; get; }

        /// <summary> 
        /// 用户设置使用的语言 
        /// </summary> 
        [StringLength(64)]
        public string Language { set; get; }

        /// <summary> 
        /// 用户设置使用的皮肤 
        /// </summary> 
        [StringLength(64)]
        public string Theme { set; get; }

        /// <summary> 
        /// 是否接收故障警报信息 
        /// </summary> 
        [DefaultValue(false)]
        public bool IsAlarm { set; get; }

        /// <summary>
        /// 创建该用户信息的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }

        /// <summary>
        ///  创建该用户信息的用户ID
        /// </summary>
        [StringLength(64)]
        public string CreatorUserId { set; get; }

        /// <summary>
        /// 最后更新该用户信息时间
        /// </summary>
        public DateTime? LastUpdatedTime { set; get; }

        /// <summary>
        /// 最后更新该用户信息的用户ID
        /// </summary>
        [StringLength(64)]
        public string LastUpdatorUserId { set; get; }

        #endregion

        #region 关联实体和关联属性
        ///// <summary>
        ///// <see cref="UserLogin"/>实体主键
        ///// </summary>
        //public Guid UserLogin_Id { set; get; }

        /// <summary>
        /// <see cref="UserLogin"/>实体对象
        /// </summary>
        public virtual UserLogin UserLoginOne { get; set; }
        #endregion
    }
}
