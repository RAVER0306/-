using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯分控的基本信息实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯分控的基本信息实体主键类型</typeparam>
    public abstract class SubControlBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 分控编号
        /// </summary>
        public int SubNum { set; get; }

        /// <summary>
        /// 获取或设置 分控名称
        /// </summary>
        [StringLength(64)]
        public string SubName { set; get; }

        /// <summary>
        /// 获取或设置 分控UID
        /// </summary>
        [StringLength(64)]
        public string UID { set; get; }

        /// <summary>
        /// 获取或设置 分控信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary> 
        ///  获取或设置 分控位置经度
        /// </summary> 
        public double Longitude { set; get; }

        /// <summary> 
        ///  获取或设置 分控位置纬度
        /// </summary> 
        public double Latitude { set; get; }
    }
}
