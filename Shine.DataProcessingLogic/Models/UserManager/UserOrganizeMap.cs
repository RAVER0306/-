using Shine.Core.Data;
using Shine.DataProcessingLogic.Base.UserManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.UserManager
{
    public class UserOrganizeMap : EntityBase<Guid>, ICreatedTime
    {
        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 实体对象主键
        /// </summary>
        public Guid UserLogin_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 实体对象
        /// </summary>
        public UserLogin UserLoginOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/> 实体信息主键
        /// </summary>
        public Guid Organize_Id { set; get; }
        
        /// <summary>
        /// 获取或设置 <see cref="Organize"/> 实体信息 
        /// </summary>
        public Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 该信息创建的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }
    }
}
