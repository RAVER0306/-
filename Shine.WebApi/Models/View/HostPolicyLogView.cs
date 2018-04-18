using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class HostPolicyLogView : EntityBase<Guid>
    {
        /// <summary>
        /// 所属组织机构名称
        /// </summary>
        public string OrganizeName { set; get; }

        /// <summary>
        /// 所属主机的名称
        /// </summary>
        public string HostName { set; get; }

        /// <summary>
        /// 所属主机的注册包
        /// </summary>
        public string HostRegPacket { set; get; }
        
        /// <summary>
        /// 当前策略状态名称
        /// </summary>

        public string DataItemDetailName { set; get; }

        /// <summary>
        /// 当前策略状态类型序号
        /// </summary>
        public byte DataItemDetailIndex { set; get; }

        /// <summary>
        /// 发送该日志信息的用户
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 对应的策略名称
        /// </summary>
        public string HostPolicyName { set; get; }

        /// <summary>
        /// 对应的策略编号
        /// </summary>

        public int HostPolicyNum { set; get; }

        /// <summary>
        /// 获取或设置 创建该信息的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }
    }
}