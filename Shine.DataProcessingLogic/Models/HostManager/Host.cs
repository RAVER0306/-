using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯主机的基本信息实体
    /// </summary>
    [Description("路灯主机的基本信息")]
    public class Host : HostBase<Guid>
    {

        public Host()
        {
            HostRealTimeDataMany = new HashSet<HostRealTimeData>();
            HostParameterMany = new HashSet<HostParameter>();
            HostLoginMany = new HashSet<HostLogin>();
            LightPlanMany = new HashSet<LightPlan>();
            HostPolicyLogMany = new HashSet<HostPolicyLog>();
            LightPoleMany = new HashSet<LightPole>();
            SubAggregationMany = new HashSet<SubAggregation>();
            HostPolicyMany = new HashSet<HostPolicy>();
        }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 主机关联 <see cref="DataItemDetail"/> 类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 主机的关联 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostRealTimeData"/>表实体数据集合
        /// </summary>
        public virtual ICollection<HostRealTimeData> HostRealTimeDataMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostParameter"/>表实体集合
        /// </summary>

        public virtual ICollection<HostParameter> HostParameterMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostLogin"/>表实体集合
        /// </summary>
        public virtual ICollection<HostLogin> HostLoginMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPlan"/>表实体集合
        /// </summary>
        public virtual ICollection<LightPlan> LightPlanMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicyLog"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<HostPolicyLog> HostPolicyLogMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<LightPole> LightPoleMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubAggregation"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<SubAggregation> SubAggregationMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicy"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<HostPolicy> HostPolicyMany { set; get; }
    }
}
