using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯分控实时数据信息实体
    /// </summary>
    [Description("路灯分控实时数据信息实体")]
    public class SubRealTimeData : SubRealTimeDataBase<Guid>
    {
        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>实体对象
        /// </summary>
        public virtual SubControl SubControlOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>实体对象主键
        /// </summary>
        public Guid SubControl_Id { set; get; }

        /// <summary>
        /// 获取或设置 在<see cref="DataItemDetail"/>中的灯具类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 在<see cref="DataItemDetail"/>中的灯具类型
        /// </summary>
        public virtual DataItemDetail DataItemDetailOne { set; get; }
    }
}