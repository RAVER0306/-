using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    /// <summary>
    /// 用于更新登录表的基本实体
    /// </summary>
    public class UpdatedUserLoginView
    {
        /// <summary>
        /// 用户登录表主键
        /// </summary>
        public Guid Id { set; get; }

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