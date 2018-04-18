﻿using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace Shine.Comman.Net
{
    /// <summary>
    /// 网络辅助操作
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// 是否能Ping通指定主机
        /// </summary>
        public static bool Ping(string ip)
        {
            try
            {
                Ping ping = new Ping();
                PingOptions options = new PingOptions { DontFragment = true };
                string data = "Test ListData";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000;
                PingReply reply = ping.Send(ip, timeout, buffer, options);
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }

        /// <summary>
        /// 网络是否畅通
        /// </summary>
        public static bool IsInternetConnected()
        {
            bool state = InternetGetConnectedState(out int i, 0);
            return state;
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
    }
}
