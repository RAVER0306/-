using Shine.Core.Data;
using Shine.DataProcessingLogic.Dtos.OrganzieManager;
using System;

namespace Shine.DataProcessingLogic.Dtos.UserManager
{
    public class UserOrganizeMapOutDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 用户管理的组织<see cref="OrganizeOutDto"/>
        /// </summary>
        public virtual OrganizeOutDto OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 该信息创建的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }
    }
}
