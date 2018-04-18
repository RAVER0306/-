/*===================================================
* 类名称: LightPlan_0x59_In
* 类描述: 隧道光照计划数据输入
* 创建人: myining
* 创建时间: 2018/2/26 17:09:49
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class LightPlan_0x59_In : EntityBase<Guid>
    {
        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        /// 获取或设置 计划编号 0-16；暂时只用第一个
        /// </summary> 
        public int PlanID { set; get; }

        /// <summary> 
        /// 获取或设置 光照计划使能（01为开，00为关） 
        /// </summary>       
        public int Enable { set; get; }

        /// <summary> 
        /// 获取或设置 比例系数，每个分组都设置一个 共16个    
        /// 存储形式：1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
        /// </summary> 
        public string ScaleFactor { set; get; }

        /// <summary> 
        /// 获取或设置 阈值 上限（16进制）光照度大于该值时关 
        /// </summary> 
        public int MaxLimit { set; get; }

        /// <summary> 
        /// 获取或设置 阈值 下限（16进制）光照度低于该值时开灯 
        /// </summary> 
        public int MinLimit { set; get; }

        /// <summary> 
        /// 获取或设置 最大亮度 
        /// </summary> 
        public int MaxBrightness { set; get; }

        /// <summary> 
        /// 获取或设置 最小亮度 
        /// </summary> 
        public int MinBrightness { set; get; }

        /// <summary> 
        /// 获取或设置 预留当做光照补偿曲线选择位 
        /// </summary> 
        public int IlluminationCurve { set; get; }

        /// <summary> 
        /// 获取或设置 触发时间 
        /// </summary> 
        public int TriggerTime { set; get; }
    }
}
