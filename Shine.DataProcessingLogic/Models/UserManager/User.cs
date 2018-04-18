using Shine.DataProcessingLogic.Base.UserManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.UserManager
{
    /// <summary>
    /// 用户基本信息表实体
    /// </summary>
    [Description("用户基本信息")]
    public class User : UserBase<Guid>
    {
        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/>实体对象的主键
        /// </summary>
        public Guid UserLogin_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/>实体对象
        /// </summary>
        public virtual UserLogin UserLoginOne { set; get; }
    }
}
