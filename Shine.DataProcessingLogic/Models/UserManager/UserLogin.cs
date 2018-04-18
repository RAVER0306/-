using Shine.DataProcessingLogic.Base.UserManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.UserManager
{
    /// <summary>
    /// 用户登录信息表实体
    /// </summary>
    [Description("用户登录信息")]
    public class UserLogin : UserLoginBase<Guid>
    {
        public UserLogin()
        {
            UserMany = new HashSet<User>();
            UserBlackListMany = new HashSet<UserBlackList>();
            UserOrganizeMapMany = new HashSet<UserOrganizeMap>();
            InformationMany = new HashSet<Information>();
        }

        /// <summary>
        /// 获取或设置 用户所属的组织<see cref="Organize"/>的主键
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 用户所属的组织<see cref="Organize"/>
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="User"/>实体集合
        /// </summary>
        public virtual ICollection<User> UserMany { set; get; }
        

        /// <summary>
        /// 获取或设置 <see cref="UserBlackList"/>实体集合
        /// </summary>
        public virtual ICollection<UserBlackList> UserBlackListMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserOrganizeMap"/> 实体集合
        /// </summary>
        public virtual ICollection<UserOrganizeMap> UserOrganizeMapMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Information"/> 实体集合
        /// </summary>
        public virtual ICollection<Information> InformationMany { set; get; }



    }
}
