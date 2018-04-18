using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Models.OrganizeManager
{
    public class UpgradeLog : EntityBase<Guid>, ICreatedTime
    {
        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 升级对象内容
        /// </summary>
        [StringLength(64)]
        public string Content { set; get; }

        /// <summary>
        /// 获取或设置 升级包的对象内容
        /// </summary>
        [StringLength(64)]
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
