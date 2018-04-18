using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯主机的策略信息实体
    /// </summary>
    [Description("路灯主机的策略信息实体")]
    public class HostPolicy: HostPolicyBase<Guid>
    {
        public HostPolicy()
        {
            HostPolicyLogMany = new HashSet<HostPolicyLog>();
        }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organzie_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体
        /// </summary>
        public virtual Host HostOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体主键
        /// </summary>
        public Guid Host_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicyLog"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<HostPolicyLog> HostPolicyLogMany { set; get; }
    }
}
