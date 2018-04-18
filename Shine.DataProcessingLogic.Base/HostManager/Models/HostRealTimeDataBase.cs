using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯主机实时更新的数据实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯主机实时更新的数据实体主键类型</typeparam>
    public abstract class HostRealTimeDataBase<TKey> :
        EntityBase<TKey>,
        ITimestamp
        where TKey : IEquatable<TKey>
    {
        /// <summary> 
        /// 获取或设置 路灯主机回路状态 
        /// 说明：存储形式用","隔开，
        /// 1代表开回路 0代表关回路 ,
        /// 如八个回路全开：1,1,1,1,1,1,1,1 
        /// </summary> 
        [StringLength(32)]
        public string LoopState { set; get; }

        /// <summary> 
        /// 获取或设置 主机三相电电压，
        /// 说明：存储形式用","隔开，
        /// 如：1,2,3
        /// </summary> 
        [StringLength(32)]
        public string Voltage { set; get; }

        /// <summary> 
        /// 获取或设置 主机三相电电流，
        /// 说明：存储形式用","隔开，
        /// 如：1,2,3
        /// </summary> 
        [StringLength(32)]
        public string Current { set; get; }

        /// <summary> 
        /// 获取或设置 主机三相电功率，
        /// 说明：存储形式用","隔开，
        /// 存储形式 - 1,2,3 
        /// </summary> 
        [StringLength(32)]
        public string Power { set; get; }

        /// <summary> 
        /// 获取或设置 主机温度 
        /// </summary> 
        [DefaultValue(0)]
        public int Temperature { set; get; }

        /// <summary> 
        /// 获取或设置 主机上传的经度值 
        /// 说明：东经为正数，西经为负数，
        /// 如：-120.1 表示西经120.1°
        /// </summary> 
        [Range(-180,180),DefaultValue(0)]
        public double Longitude { set; get; }

        /// <summary> 
        /// 获取或设置 主机上传的纬度值 
        /// 说明：南为正数，北经为负数，
        /// 如：90 表示南纬90°
        /// </summary> 
        [Range(-90,90),DefaultValue(0)]
        public double Latitude { set; get; }

        /// <summary> 
        /// 获取或设置 主机所在时区 
        /// 说明：东1-12区为正数，西1-12区为负数，0为默认值，
        /// 如：-8 表示西8区
        /// </summary> 
        [DefaultValue(0),Range(-12,12)]
        public int TimeZone { set; get; }

        /// <summary> 
        /// 获取或设置 主机是否在线 
        /// </summary> 
        public bool IsOnline { set; get; }

        /// <summary> 
        /// 获取或设置 主机信息最后更新的时间 
        /// </summary> 
        [DefaultValue("1990-1-1")]
        public DateTime UpdateTime { set; get; }

        /// <summary> 
        /// 获取或设置 该主机的断线警报是否已经发送
        /// </summary> 
        [DefaultValue(false)]
        public bool IsAlarmMark { set; get; }

        /// <summary>
        /// 获取或设置 该主机的的数据错误信息是否发送
        /// </summary>
        [DefaultValue(false)]
        public bool IsDataError { set; get; }

        /// <summary>
        /// 获取或设置 版本控制标识，用于处理并发
        /// </summary>
        public byte[] Timestamp { get; set; }
    }
}
