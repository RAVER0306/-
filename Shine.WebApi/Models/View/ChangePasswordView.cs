using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    /// <summary>
    /// 修改用户密码的实体
    /// </summary>
    public class ChangePasswordView
    { 
        /// <summary>
        /// 准备修改密码的用户主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 该用户修改密码前的旧密码
        /// </summary>
        public string OldPassword { set; get; }

        /// <summary>
        /// 该用户修改密码后的新密码
        /// </summary>
        public string NewPassword { set; get; }
    }
}