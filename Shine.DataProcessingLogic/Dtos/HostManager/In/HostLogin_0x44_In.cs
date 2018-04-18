/*===================================================
* 类名称: HostLogin_0x44_In
* 类描述: 主机登陆信息更新输入实体
* 创建人: myining
* 创建时间: 2018/3/12 11:23:47
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using Shine.Core.Data;
using System;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class HostLogin_0x44_In : EntityBase<Guid>
    {
        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        /// 获取或设置 连接到WIFI的名称
        /// </summary> 
        public string WifiName { set; get; }

        /// <summary> 
        /// 获取或设置 模式 0：WPAPSK 1：WPAPS2K 
        /// </summary> 
        public byte WifiMode { set; get; }

        /// <summary> 
        /// 获取或设置 WIFI加密算法 0：AES 1：TKIP 
        /// </summary> 
        public byte WifiAlgorithm { set; get; }

        /// <summary> 
        /// 获取或设置 wifi密码 最长20 
        /// </summary> 
        public string WifiPassword { set; get; }

        /// <summary> 
        /// 获取或设置 主机要连接到的服务器IP
        /// </summary> 
        public string ServerIp { set; get; }

        /// <summary> 
        /// 获取或设置 服务器端口
        /// </summary> 
        public string ServerPort { set; get; }

        /// <summary> 
        /// 获取或设置 VPN接入参数
        /// </summary> 
        public string ServerVpn { set; get; }
    }
}
