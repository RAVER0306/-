using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class HostLoginPageOutDto : IOutputDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 关联主机的主键
        /// </summary>

        public Guid Host_Id { set; get; }

        /// <summary>
        /// 关联主机注册包
        /// </summary>

        public string HostRegPacket { set; get; }

        /// <summary>
        /// 关联主机名称
        /// </summary>
        public string HostName { set; get; }

        /// <summary> 
        /// 获取或设置 连接到WIFI的名称
        /// </summary> 
        [StringLength(32)]
        public string WifiName { set; get; }

        /// <summary> 
        /// 获取或设置 模式 0：WPAPSK 1：WPAPS2K 
        /// </summary> 
        [DefaultValue(0), Range(0, 1)]
        public byte WifiMode { set; get; }

        /// <summary> 
        /// 获取或设置 WIFI加密算法 0：AES 1：TKIP 
        /// </summary> 
        [DefaultValue(0), Range(0, 1)]
        public byte WifiAlgorithm { set; get; }

        /// <summary> 
        /// 获取或设置 wifi密码 最长20 
        /// </summary> 
        [StringLength(32)]
        public string WifiPassword { set; get; }

        /// <summary> 
        /// 获取或设置 主机要连接到的服务器IP
        /// </summary> 
        [StringLength(32)]
        public string ServerIp { set; get; }

        /// <summary> 
        /// 获取或设置 服务器端口
        /// </summary> 
        [StringLength(16)]
        public string ServerPort { set; get; }

        /// <summary> 
        /// 获取或设置 VPN接入参数
        /// </summary> 
        [StringLength(64)]
        public string ServerVpn { set; get; }

        /// <summary> 
        /// 获取或设置 最后更新信息的时间 
        /// </summary> 
        [DefaultValue("1990-01-01")]
        public DateTime UpdateTime { set; get; }
    }
}