using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
   public class LightPlanInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { set; get; }

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
        public string LoopState { set; get; }

        /// <summary> 
        /// 获取或设置 光照计划执行时的亮度 
        /// </summary> 
        public int Brightness { set; get; }

        /// <summary> 
        /// 获取或设置 自动控制开关 
        /// 0：关闭 （计划按照固定的亮度执行）,
        /// 1：开启（计划按照线性补偿执行） 
        /// </summary> 
        public int AutoSwitch { set; get; }

        /// <summary> 
        /// 获取或设置 预留当做光照补偿曲线选择位 
        /// </summary> 
        public int IlluminationCurve { set; get; }

        /// <summary> 
        /// 获取或设置 执行光照计划的分组(解析后的分组数据)
        /// </summary> 
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
        /// 获取或设置 <see cref="DataItemDetail"/>表数据实体主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体主键
        /// </summary>
        public Guid Host_Id { set; get; }
    }
}
