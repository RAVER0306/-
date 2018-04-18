using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯分控的基本信息实体
    /// </summary>
    [Description("路灯分控的基本信息实体")]
    public class SubControl: SubControlBase<Guid>
    {
        public SubControl()
        {
            SubRealTimeDataMany = new HashSet<SubRealTimeData>();
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
        /// 获取或设置 <see cref="SubRealTimeData"/>对象的实体集合
        /// </summary>
        public virtual ICollection<SubRealTimeData> SubRealTimeDataMany { set; get; }

        /// 获取或设置 <see cref="SubAggregation"/>对象的实体集合
        /// </summary>
        public virtual ICollection<SubAggregation> SubAggregationMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>对象的实体集合
        /// </summary>
        public virtual LightPole LigthPoleOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>关联主键
        /// </summary>
        public Guid LigthPoleOne_Id { set; get; }
    }
}
