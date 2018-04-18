using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Dtos
{
    /// <summary>
    /// 用户登录输入Dtos
    /// </summary>
    public class UserLoginInputDto : IInputDto
    {
        #region 指定 UserLogin 实体的基础属性-用于数据映射

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
        /// 获取或设置 用户是否被锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 是否允许登录锁定
        /// </summary>
        public bool LockoutEnabled { get; set; }

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
        #endregion

        #region UserLogin 扩展输入属性

        /// <summary>
        /// 获取或设置 该用户所属的组织机构ID
        /// </summary>
        public Guid Organize_Id { set; get; }
        #endregion
    }
}
