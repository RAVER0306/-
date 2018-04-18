using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯灯杆的基本信息实体
    /// </summary>
    [Description("路灯灯杆的基本信息实体")]
    public class LightPole: LightPoleBase<Guid>
    {
        public LightPole()
        {
            SubControlMany = new HashSet<SubControl>();
            SubAggregationMany = new HashSet<SubAggregation>();
        }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/>表数据实体
        /// </summary>

        public virtual DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/>表数据实体主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<SubControl> SubControlMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubAggregation"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<SubAggregation> SubAggregationMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体
        /// </summary>
        public virtual Host HostOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体主键
        /// </summary>
        public Guid Host_Id { set; get; }

    }
}
