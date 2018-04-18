using Shine.Core.Data;
using Shine.DataProcessingLogic.Dtos.OrganzieManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models.OutDtos
{
    public class UpgradeLogOutDto:EntityBase<Guid>, IOutputDto
    {
        /// <summary>
        /// 获取或设置 <see cref="Organize"/>主键
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>名称
        /// </summary>
        public string OrganizeName { set; get; }

        /// <summary>
        /// 获取或设置 升级对象内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 获取或设置 升级包的对象内容
        /// </summary>
        public string Packet { set; get; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 升级状态
        /// 1=任务计划后台执行中
        /// 2=任务计划超时被暂停
        /// 3=任务计划执行已完成
        /// </summary>
        public short State { set; get; }

        /// <summary>
        /// 获取或设置 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 获取或设置 创建升级的用户信息
        /// </summary>
        public string CreatorUserId { set; get; }
    }
}