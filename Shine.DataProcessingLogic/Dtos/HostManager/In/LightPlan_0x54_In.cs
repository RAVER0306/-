/*===================================================
* 类名称: LightPlan_0x54_In
* 类描述: 标准光照计划数据输入
* 创建人: myining
* 创建时间: 2018/2/26 16:09:24
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
    public class LightPlan_0x54_In : EntityBase<Guid>
    {
        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        /// 获取或设置 光照计划使能（01为开，00为关） 
        /// </summary>       
        public int Enable { set; get; }

        /// <summary> 
        /// 获取或设置 是否群控开关（01为开，00为关//开启时默认控制全部回路与分组，关闭时控制下面选择的回路与分组） 
        /// </summary> 
        public int GroupSwitch { set; get; }

        /// <summary>
        /// 获取或设置 执行光照计划的回路值（十进制）
        /// </summary>
        public byte BitLoopState { set; get; }

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
    }
}
