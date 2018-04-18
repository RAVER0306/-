using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯灯杆基本信息实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯灯杆基本信息实体主键类型</typeparam>
    public abstract class LightPoleBase<TKey>:
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 灯杆编号
        /// </summary>
        public int PoleNum { set; get; }

        /// <summary>
        /// 获取或设置灯杆名称
        /// </summary>
        [StringLength(64)]
        public string PoleName { set; get; }

        /// <summary>
        /// 获取或设置 灯杆信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 灯杆的地址
        /// </summary>

        [StringLength(128)]
        public string Address { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
