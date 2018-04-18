using Shine.DataProcessingLogic.Base.HostManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯主机的基本参数设置信息
    /// </summary>
    [Description("路灯主机的基本参数设置信息")]
    public class HostParameter : HostParameterBase<Guid>
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
