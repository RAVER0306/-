﻿using System;
using System.Runtime.InteropServices;

namespace Shine.Comman.HPSocket
{
    /// <summary>
    /// SSL 工作模式
    /// 描述：标识 SSL 的工作模式，客户端模式或服务端模式
    /// </summary>
    public enum SSLSessionMode
    {
        /// <summary>
        /// 客户端模式
        /// </summary>
        Client = 0,
        /// <summary>
        /// 服务端模式
        /// </summary>
        Server = 1,
    }

    /// <summary>
    /// 名称：SSL 验证模式
    /// 描述：SSL 验证模式选项，SSL_VM_PEER 可以和后面两个选项组合一起
    /// </summary>
    public enum SSLVerifyMode
    {
        /// <summary>
        /// SSL_VERIFY_NONE
        /// </summary>
        None = 0x00,
        /// <summary>
        /// SSL_VERIFY_PEER
        /// </summary>
        Peer = 0x01,
        /// <summary>
        /// SSL_VERIFY_FAIL_IF_NO_PEER_CERT
        /// </summary>
        FailIfNoPeerCert = 0x02,
        /// <summary>
        /// SSL_VERIFY_CLIENT_ONCE
        /// </summary>
        ClientOnce = 0x04,
    }

    /// <summary>
    /// Unicode版本
    /// </summary>
    public class SSLSdk
    {
        /// <summary>
        /// 名称：SNI 服务名称回调函数
        /// 描述：根据服务器名称选择 SSL 证书
        /// 返回值：
		/// 0	 -- 成功，使用默认 SSL 证书
        /// 正数	 -- 成功，使用返回值对应的 SNI 主机证书
        /// 负数	 -- 失败，中断 SSL 握手
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public delegate int SNIServerNameCallback(string serverName);

        /// <summary>
        /// HPSocket的文件路径
        /// </summary>
        public const string HPSOCKET_SSL_DLL_PATH = "HPSocket4C-SSL_U.dll";

        /**************** HPSocket4C-SSL 导出函数 ****************/
        /// <summary>
        /// 创建 HP_SSLServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLServer(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLAgent(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLClient(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPullServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLPullServer(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPullAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLPullAgent(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPullClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLPullClient(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPackServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLPackServer(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPackAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLPackAgent(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPackClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern IntPtr Create_HP_SSLPackClient(IntPtr pListener);



        /// <summary>
        /// 销毁 HP_SSLServer 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLServer(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLAgent 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLAgent(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLClient 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLClient(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPullServer 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLPullServer(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPullAgent 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLPullAgent(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPullClient 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLPullClient(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPackServer 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLPackServer(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPackAgent 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLPackAgent(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPackClient 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void Destroy_HP_SSLPackClient(IntPtr pObj);

        /************************ SSL 初始化方法 ****************************/
        /// <summary>
        /// 名称：初始化 SSL 全局环境参数
        /// 描述：SSL 全局环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="sessionMode">SSL 工作模式</param>
        /// <param name="verifyMode">SSL 验证模式</param>
        /// <param name="lpszPemCertFile">证书文件（客户端可选）</param>
        /// <param name="lpszPemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="lpszKeyPasswod">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCertFileOrPath">CA 证书文件或目录（单向验证或客户端可选）</param>
        /// <param name="fnServerNameCallback">SNI 回调函数指针（可选，只用于服务端）</param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool HP_SSL_Initialize(SSLSessionMode sessionMode, SSLVerifyMode verifyMode, string lpszPemCertFile, string lpszPemKeyFile, string lpszKeyPasswod, string lpszCAPemCertFileOrPath, SSLSdk.SNIServerNameCallback fnServerNameCallback);

        /// <summary>
        /// 清理 SSL 全局运行环境
        /// 描述：清理 SSL 全局运行环境，回收 SSL 相关内存
        ///		1、应用程序退出时会自动调用调用本方法
        ///		2、当要重新设置 SSL 全局环境参数时，需要先调用本方法清理原先的环境参数
        /// </summary>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void HP_SSL_Cleanup();


        /// <summary>
        /// 清理线程局部环境 SSL 资源
        /// 描述：清理 SSL 全局运行环境，回收 SSL 相关内存
        ///		任何一个操作 SSL 的线程，通信结束时都需要清理线程局部环境 SSL 资源
        ///		1、主线程和 HP-Socket 工作线程在通信结束时会自动清理线程局部环境 SSL 资源。因此，一般情况下不必手工调用本方法
        ///		2、特殊情况下，当自定义线程参与 HP-Socket 通信操作并检查到 SSL 内存泄漏时，需在每次通信结束时自定义线程调用本方法
        /// </summary>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern void HP_SSL_RemoveThreadLocalState();

        /// <summary>
        /// 检查 SSL 全局运行环境是否初始化完成
        /// </summary>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH)]
        public static extern bool HP_SSL_IsValid();
        

        /// <summary>
        /// 名称：增加 SNI 主机证书（只用于服务端）
        /// 描述：SSL 服务端在 Initialize() 成功后可以调用本方法增加多个 SNI 主机证书
        /// 成功：正数, 返回 SNI 主机证书对应的索引，该索引用于在 SNI 回调函数中定位 SNI 主机
        /// 失败：负数, 可通过 SYS_GetLastError() 获取失败原因
        /// </summary>
        /// <param name="iVerifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCertFile">证书文件</param>
        /// <param name="lpszPemKeyFile">私钥文件</param>
        /// <param name="lpszKeyPasswod">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCertFileOrPath">CA 证书文件或目录（单向验证可选）</param>
        /// <returns></returns>
        [DllImport(HPSOCKET_SSL_DLL_PATH, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int HP_SSL_AddServerContext(SSLVerifyMode verifyMode, string lpszPemCertFile, string lpszPemKeyFile, string lpszKeyPasswod /* nullptr */, string lpszCAPemCertFileOrPath /* nullptr */);

    }
}
