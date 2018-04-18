using Shine.Core.Data;
using Shine.DataProcessingLogic.Dtos.OrganzieManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Dtos.UserManager
{
    public class UserLoginOutDto : IOutputDto
    {
        public UserLoginOutDto()
        {
            UserMany = new HashSet<UserOutDto>();
            InformationMany = new HashSet<InformationOutDto>();
        }

        #region 基础参数
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 用户账号
        /// </summary>
        [Required, StringLength(32)]
        public string UserName { set; get; }

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

        #region 扩展参数
        /// <summary>
        /// 获取或设置 用户所属的组织<see cref="OrganizeOutDto"/>
        /// </summary>
        public virtual OrganizeOutDto OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 用户的基础信息 <see cref="UserOutDto"/>
        /// </summary>
        public virtual ICollection<UserOutDto> UserMany { set; get; }

        /// <summary>
        /// 获取或设置 用户的系统信息 <see cref="InformationOutDto"/> 
        /// </summary>
        public virtual ICollection<InformationOutDto> InformationMany { set; get; }
        #endregion

    }
}
