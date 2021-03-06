﻿using System;

namespace Shine.Comman.HPSocket
{
    public class SSLPullAgent : SSLAgent
    {
        protected new TcpPullAgentEvent.OnReceiveEventHandler OnReceive;

        public SSLPullAgent()
            : base()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_verifyModel">验证模式</param>
        /// <param name="_pemCertFile">证书文件（客户端可选）</param>
        /// <param name="_pemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="_keyPasswod">私钥密码（没有密码则为空）</param>
        /// <param name="_caPemCertFileOrPath">CA 证书文件或目录（单向验证或客户端可选）</param>
        public SSLPullAgent(SSLVerifyMode _verifyModel, string _pemCertFile, string _pemKeyFile, string _keyPasswod, string _caPemCertFileOrPath)
            : base(_verifyModel, _pemCertFile, _pemKeyFile, _keyPasswod, _caPemCertFileOrPath)
        {

        }

        /// <summary>
        /// 创建socket监听&服务组件
        /// </summary>
        /// <returns></returns>
        protected override bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || PAgent != IntPtr.Zero)
            {
                return false;
            }

            pListener = Sdk.Create_HP_TcpPullAgentListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            PAgent = SSLSdk.Create_HP_SSLPullAgent(pListener);
            if (PAgent == IntPtr.Zero)
            {
                return false;
            }

            IsCreate = true;

            return true;
        }

        /// <summary>
        /// 抓取数据
        /// 用户通过该方法从 Socket 组件中抓取数据
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="pBuffer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public FetchResult Fetch(IntPtr connId, IntPtr pBuffer, int size)
        {
            return Sdk.HP_TcpPullAgent_Fetch(PAgent, connId, pBuffer, size);
        }

        /// <summary>
        /// 抓取数据
        /// 用户通过该方法从 Socket 组件中抓取数据
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="pBuffer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public FetchResult Peek(IntPtr connId, IntPtr pBuffer, int size)
        {
            return Sdk.HP_TcpPullAgent_Peek(PAgent, connId, pBuffer, size);
        }

        new Sdk.OnPullReceive _OnReceive = null;

        /// <summary>
        /// 设置回调函数
        /// </summary>
        /// <param name="prepareConnect"></param>
        /// <param name="connect"></param>
        /// <param name="send"></param>
        /// <param name="recv"></param>
        /// <param name="close"></param>
        /// <param name="error"></param>
        /// <param name="agentShutdown"></param>
        protected override void SetCallback()
        {
            _OnReceive = new Sdk.OnPullReceive(SDK_OnReceive);

            // 设置 Socket 监听器回调函数
            Sdk.HP_Set_FN_Agent_OnPullReceive(pListener, _OnReceive);
            base.SetCallback();
        }

        protected virtual HandleResult SDK_OnReceive(IntPtr pSender,IntPtr connId, int length)
        {
            if (OnReceive != null)
            {
                return OnReceive(connId, length);
            }
            return HandleResult.Ignore;
        }

        /// <summary>
        /// 终止服务并释放资源
        /// </summary>
        public override void Destroy()
        {
            Stop();

            if (PAgent != IntPtr.Zero)
            {
                SSLSdk.Destroy_HP_SSLPullAgent(PAgent);
                PAgent = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                Sdk.Destroy_HP_TcpPullAgentListener(pListener);
                pListener = IntPtr.Zero;
            }

            IsCreate = false;
        }
    }
}
