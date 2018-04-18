using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯主机的策略下载日志实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯主机的策略下载日志实体主键类型</typeparam>
    public abstract class HostPolicyLogBase<TKey>:
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 创建该信息的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }
    }
}
