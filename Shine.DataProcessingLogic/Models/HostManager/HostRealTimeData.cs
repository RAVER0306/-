using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 远程路灯主机上传的基本信息
    /// </summary>
    [Description("远程路灯主机上传的基本信息")]
    public class HostRealTimeData : HostRealTimeDataBase<Guid>
    {
        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体
        /// </summary>
        public virtual Host HostOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体主键
        /// </summary>
        public virtual Guid Host_Id { set; get; }
    }
}
