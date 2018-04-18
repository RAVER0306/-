using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯主机的光照计划实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯主机的光照计划实体主键类型</typeparam>
    public abstract class LightPlanBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        private string _loopState;
        private byte _bitLoopState;

        /// <summary> 
        /// 获取或设置 光照计划使能（01为开，00为关） 
        /// </summary>       
        [Range(0, 1)]
        public int Enable { set; get; }

        /// <summary> 
        /// 获取或设置 是否群控开关（01为开，00为关//开启时默认控制全部回路与分组，关闭时控制下面选择的回路与分组） 
        /// </summary> 
        [Range(0, 1)]
        public int GroupSwitch { set; get; }

        /// <summary> 
        /// 获取或设置 执行光照计划的回路
        /// 说明：存储形式用","隔开，
        /// 1代表开回路 0代表关回路 ,
        /// 如八个回路全开：1,1,1,1,1,1,1,1 
        /// </summary> 
        [StringLength(16)]
        [RegularExpression("([0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1])")]
        public string LoopState
        {
            set
            {
                // value=1,1,1,1,1,1,1,1
                var loops = value.Split(',');
                if (loops.Length == 8)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int max = loops.Length - 1; max >= 0; max--)
                    {
                        builder.Append(loops[max]);
                    }
                    _bitLoopState = (byte)Convert.ToInt32(builder.ToString(), 2);
                }
                _loopState = value;
            }
            get
            {
                return _loopState;
            }
        }

        /// <summary>
        /// 获取或设置 执行光照计划的回路值（十进制）
        /// </summary>
        [Range(0, 255)]
        public byte BitLoopState { get => _bitLoopState; set
            {
                _bitLoopState = value;
                var loops = Convert.ToString(value, 2).PadLeft(8, '0');
                StringBuilder sb = new StringBuilder();
                for (int n = loops.Length - 1; n >= 0; n--)
                {
                    if (n == 0)
                    {
                        sb.Append($"{loops.Substring(n, 1)}");
                    }
                    else
                    {
                        sb.Append($"{loops.Substring(n, 1)},");
                    }
                }
                _loopState = sb.ToString();
            }
        }

        /// <summary> 
        /// 获取或设置 光照计划执行时的亮度 
        /// </summary> 
        public int Brightness { set; get; }

        /// <summary> 
        /// 获取或设置 自动控制开关 
        /// 0：关闭 （计划按照固定的亮度执行）,
        /// 1：开启（计划按照线性补偿执行） 
        /// </summary> 
        [Range(0, 1)]
        public int AutoSwitch { set; get; }

        /// <summary> 
        /// 获取或设置 预留当做光照补偿曲线选择位 
        /// </summary> 
        public int IlluminationCurve { set; get; }

        /// <summary> 
        /// 获取或设置 执行光照计划的分组(解析后的分组数据)
        /// </summary> 
        [RegularExpression("([0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1],[0-1])")]
        public string GroupTexs { set; get; }

        /// <summary> 
        /// 获取或设置 阈值 上限（16进制）光照度大于该值时关 
        /// </summary> 
        public int MaxLimit { set; get; }

        /// <summary> 
        /// 获取或设置 阈值 下限（16进制）光照度低于该值时开灯 
        /// </summary> 
        public int MinLimit { set; get; }

        /// <summary> 
        /// 获取或设置 主机当前的光照度 
        /// </summary> 
        public int CurrentBrightness { set; get; }

        /// <summary> 
        /// 获取或设置 计划编号 0-16；暂时只用第一个
        /// </summary> 
        public int PlanID { set; get; }

        /// <summary> 
        /// 获取或设置 比例系数，每个分组都设置一个 共16个    
        /// 存储形式：1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
        /// </summary> 
        public string ScaleFactor { set; get; }

        /// <summary> 
        /// 获取或设置 最大亮度 
        /// </summary> 
        public int MaxBrightness { set; get; }

        /// <summary> 
        /// 获取或设置 最小亮度 
        /// </summary> 
        public int MinBrightness { set; get; }

        /// <summary> 
        /// 获取或设置 触发时间 
        /// </summary> 
        public int TriggerTime { set; get; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 光照计划最后更新的时间
        /// </summary>
        public DateTime UpdatedTime { set; get; }
    }
}
