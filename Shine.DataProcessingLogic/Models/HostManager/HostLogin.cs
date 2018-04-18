using Shine.DataProcessingLogic.Base.HostManager.Models;
using System;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯主机的连接参数实体
    /// </summary>
    [Description("路灯主机的基本信息")]
    public class HostLogin:HostLoginBase<Guid>
    {
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
