using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯主机的基本参数设置实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯主机的基本信息实体主键类型</typeparam>
    [Description("路灯主机的基本参数设置实体基类")]
    public abstract class HostParameterBase<TKey> :
         EntityBase<TKey>
         where TKey : IEquatable<TKey>
    {
        /// <summary> 
        /// 获取或设置 主机主控频段-网络号 
        /// </summary> 
        [Range(1,255255)]
        public int NetworkNumber { set; get; }

        /// <summary> 
        /// 获取或设置 主机系统容量-灯杆数（最大500） 
        /// </summary> 
        [Range(1, 500)]
        public int PoleNumber { set; get; }

        /// <summary> 
        /// 获取或设置 主控频段-频道号 
        /// </summary> 
        [Range(11,26)]
        public int ChannelNumber { set; get; }

        /// <summary> 
        /// 获取或设置 系统容量-分组数量（最大16） 
        /// </summary> 
        [Range(1, 16)]
        public int GroupNumber { set; get; }

        /// <summary> 
        /// 获取或设置 系统语言（要注明是主机的）0：英文 1：中文 
        /// </summary> 
        [Range(0, 1)]
        public int SystemLanguage { set; get; }

        /// <summary> 
        /// 获取或设置 按键声音 0：关 1：开 
        /// </summary> 
        [Range(0, 1)]
        public int ButtonSound { set; get; }

        /// <summary> 
        /// 获取或设置 6位开机密码 
        /// </summary> 
        [StringLength(16)]
        public string StartPassword { set; get; }

        /// <summary> 
        /// 获取或设置 主机网络选择 1:移动网络  2：网线  3：wifi 
        /// </summary> 
        [Range(1, 3)]
        public int NetworkSelection { set; get; }

        /// <summary> 
        /// 获取或设置 互感线圈-初级（1-100A） 
        /// </summary> 
        [Range(1, 100)]
        public int MutualInductanceLineOne { set; get; }

        /// <summary> 
        /// 获取或设置 互感线圈-次级（1-20mA） 
        /// </summary> 
        [Range(1, 20)]
        public int MutualInductanceLineTwo { set; get; }

        /// <summary>
        /// 主机信息上传更新间隔
        /// </summary>
        [Range(1,255)]
        public int UpdateInterval { set; get; }


        /// <summary>
        /// 警报电压上限
        /// </summary>
        [Range(1,0xffff)]
        public int AlarmVoltageUpperLimit { set; get; }

        /// <summary>
        /// 警报电压下限
        /// </summary>
        [Range(1,0xffff)]
        public int AlarmVoltageLowerLimit { set; get; }
        
        /// <summary> 
        /// 获取或设置 信息最后的更新时间 
        /// </summary> 
        [DefaultValue("1990-01-01")]
        public DateTime UpdateTime { set; get; }
    }
}
