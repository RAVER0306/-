using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯分控实时信息的基类
    /// </summary>
    /// <typeparam name="TKey">路灯分控实时信息实体主键</typeparam>
    public abstract class SubRealTimeDataBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 备注名
        /// </summary>
        [StringLength(64)]
        public string FullName { set; get; }

        /// <summary> 
        /// 获取或设置 灯具在分控上的调光端口 
        /// </summary> 
        public int DimmingPort { set; get; }

        /// <summary> 
        /// 获取或设置 灯具亮度 
        /// </summary> 
        public int Brightness { set; get; }

        /// <summary> 
        /// 获取或设置 灯具电压 
        /// </summary> 
        public double Voltage { set; get; }

        /// <summary> 
        /// 获取或设置 灯具电流 
        /// </summary> 
        public double Current { set; get; }

        /// <summary> 
        /// 获取或设置 灯具功率 
        /// </summary> 
        public double Power { set; get; }

        /// <summary> 
        /// 获取或设置 灯具温度 
        /// </summary> 
        public int Temperature { set; get; }

        /// <summary> 
        /// 获取或设置 灯具频率 
        /// </summary> 
        public int Frequency { set; get; }

        /// <summary> 
        /// 获取或设置 灯具信息最后更新的时间 
        /// </summary> 
        public DateTime UpdateTime { set; get; }

        /// <summary> 
        /// 获取或设置 该灯具有警报信息时是否已经发送警报信息
        /// </summary> 
        public bool IsAlarmMark { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
