using System;

namespace Shine.Comman.HPSocket
{
    public class TcpPackServer<T> : TcpPackServer
    {
        public new T GetExtra(IntPtr connId)
        {
            return base.GetExtra<T>(connId);
        }

        public bool SetExtra(IntPtr connId, T obj)
        {
            return base.SetExtra(connId, obj);
        }
    }

    public class TcpPackServer : TcpServer
    {

        /// <summary>
        /// 创建socket监听&服务组件
        /// </summary>
        /// <returns></returns>
        protected override bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || PServer != IntPtr.Zero)
            {
                return false;
            }

            pListener = Sdk.Create_HP_TcpPackServerListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            PServer = Sdk.Create_HP_TcpPackServer(pListener);
            if (PServer == IntPtr.Zero)
            {
                return false;
            }
            
            IsCreate = true;

            return true;
        }

        /// <summary>
        /// 终止服务并释放资源
        /// </summary>
        public override void Destroy()
        {
            Stop();

            if (PServer != IntPtr.Zero)
            {
                Sdk.Destroy_HP_TcpPackServer(PServer);
                PServer = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                Sdk.Destroy_HP_TcpPackServerListener(pListener);
                pListener = IntPtr.Zero;
            }

            IsCreate = false;
        }

        /// <summary>
        /// 读取或设置数据包最大长度
        /// 有效数据包最大长度不能超过 4194303/0x3FFFFF 字节，默认：262144/0x40000
        /// </summary>
        public uint MaxPackSize
        {
            get
            {
                return Sdk.HP_TcpPackServer_GetMaxPackSize(PServer);
            }
            set
            {
                Sdk.HP_TcpPackServer_SetMaxPackSize(PServer, value );
            }
        }

        /// <summary>
        /// 读取或设置包头标识
        /// 有效包头标识取值范围  0 ~ 1023/0x3FF，当包头标识为 0 时不校验包头，默认：0
        /// </summary>
        public ushort PackHeaderFlag
        {
            get
            {
                return Sdk.HP_TcpPackServer_GetPackHeaderFlag(PServer);
            }
            set
            {
                Sdk.HP_TcpPackServer_SetPackHeaderFlag(PServer, value);
            }
        }

        
    }
}
