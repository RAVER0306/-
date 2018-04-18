using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.UserManager.In
{
    public class UserPageIn
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 是否被冻结帐号
        /// </summary>
        public bool IsLocked { set; get; }

        /// <summary>
        /// 是否设置登录密码错误锁
        /// </summary>
        public bool LockoutEnabled { set; get; }

        /// <summary>
        /// 用户所属组织机构的名称
        /// </summary>
        public string OrganizeName { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { set; get; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string PhoneNumber { set; get; }

        /// <summary>
        /// 用户创建时间
        /// </summary>
        public DateTime CreatedTime { set; get; }
        /// <summary>
        /// 帐号登录次数
        /// </summary>
        public int LoginCount { set; get; }

        /// <summary>
        /// 用户所属组织机构ID
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 用户访问权限
        /// </summary>
        public string PermissionList { get; set; }

        /// <summary>
        /// 用户权限等级
        /// </summary>
        [Range(2, 3)]
        public byte Level { set; get; }

        /// <summary>
        /// 最后更新用户
        /// </summary>
        public string LastUpdatorUserId { set; get; }
    }
}
