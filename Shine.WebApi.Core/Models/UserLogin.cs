using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.WebApi.Core.Models
{
    [Description("用户登录验证信息")]
    public class UserLogin : EntityBase<Guid>, ILockable
    {
        #region 构造函数
        /// <summary>
        /// 构造一个<see cref="UserLogin"/>实体
        /// </summary>
        public UserLogin()
        {
            UserMany = new HashSet<User>();
        }
        #endregion

        #region 属性
        /// <summary> 
        /// 用户账号 
        /// </summary> 
        [Required,StringLength(64)]
        public string Account { set; get; }

        /// <summary> 
        /// 用户密码 
        /// </summary> 
        [Required, StringLength(64)]
        public string Password { set; get; }

        /// <summary> 
        /// 用户加密密钥 
        /// </summary> 
        [Required, StringLength(128)]
        public string Secretkey { set; get; }

        /// <summary> 
        /// 用户登陆次数 
        /// </summary> 
        [DefaultValue(0)]
        public int LoginCount { set; get; }

        /// <summary> 
        /// 第一次登陆时间 
        /// </summary> 
        public DateTime? FirstVisitTime { set; get; }

        /// <summary> 
        /// 上一次登陆时间 
        /// </summary> 
        public DateTime? PreviousVisitTime { set; get; }

        /// <summary> 
        /// 最后登陆时间 
        /// </summary> 
        public DateTime? LastVisitTime { set; get; }

        /// <summary> 
        /// 是否系统超级管理员 
        /// </summary> 
        [DefaultValue(false)]
        public bool IsAdministrator { set; get; }

        /// <summary> 
        /// 是否可以同时登陆多个账号 
        /// </summary> 
        [DefaultValue(true)]
        public bool IsMultiUserLogin { set; get; }

        /// <summary> 
        /// 用户访问权限列表 
        /// </summary> 
        [StringLength(512)]
        public string AuthorizationList { set; get; }

        /// <summary> 
        /// 用户操作等级 
        /// </summary> 
        [Range(1,3)]
        [DefaultValue(3)]
        public byte? Level { set; get; }

        /// <summary>
        /// 用户帐号是否被标记锁定
        /// </summary>
        [DefaultValue(false)]
        public bool IsLocked { set; get; }
        #endregion

        #region 关联实体或关联属性

        /// <summary>
        ///  <see cref="User"/>实体集合
        /// </summary>
        public virtual ICollection<User> UserMany { get; set; }
        #endregion
    }
}
