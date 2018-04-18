using Shine.Comman.DataAnnotations;
using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.UserManager.Models
{
    /// <summary>
    /// 用户登录信息基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class UserLoginBase<TKey> :
        EntityBase<TKey>,
        ILockable
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 用户账号
        /// </summary>
        [Required, StringLength(32)]
        public string UserName { set; get; }

        /// <summary>
        /// 获取或设置 用户密码
        /// </summary>
        [Required, StringLength(64)]
        public string Password { set; get; }

        /// <summary>
        /// 获取或设置 用户密码加密密匙
        /// </summary>
        [Required, StringLength(128)]
        public string SecretKey { set; get; }

        /// <summary>
        /// 获取或设置 用户登陆次数
        /// </summary>
        [DefaultValue(0)]
        public int LoginCount { set; get; }

        /// <summary>
        /// 获取或设置 用户第一次登陆的时间
        /// </summary>
        public DateTime FirstVisitTime { set; get; }

        /// <summary>
        /// 获取或设置 用户上一次登陆的时间
        /// </summary>
        public DateTime PreviousVisitTime { set; get; }

        /// <summary>
        /// 获取或设置 用户最后一次登陆的时间
        /// </summary>
        public DateTime LastVisitTime { set; get; }

        /// <summary>
        /// 获取或设置 是否用户管理员
        /// </summary>
        [DefaultValue(false)]
        public bool IsAdministrator { set; get; }

        /// <summary>
        /// 获取或设置 用户是否被锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 登录锁定UTC时间，在此时间前登录将被锁定
        /// </summary>
        public DateTime LockoutEndDateUtc { get; set; }

        /// <summary>
        /// 获取或设置 是否允许登录锁定
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 获取或设置 当前登录失败次数，达到设定值将被锁定
        /// </summary>
        [DefaultValue(10)]
        public int AccessFailedCount { get; set; }

        /// <summary>
        ///  获取或设置 用户访问列表序列号
        ///  存储形式:用户","隔开
        /// </summary>
        [StringLength(512)]
        public string PermissionList { set; get; }

        /// <summary>
        /// 获取或设置 用户权限等级
        /// 说明：Level = 1超级管理员，
        ///       Level = 2管理员，
        ///       Level = 3普通用户
        /// </summary>
        [Range(1, 3)]
        public byte Level { set; get; }
    }
}
