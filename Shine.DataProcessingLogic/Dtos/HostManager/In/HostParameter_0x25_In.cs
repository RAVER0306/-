/*===================================================
* 类名称: HostParameter_0x25_In
* 类描述: 主机系统参数更新输入实体
* 创建人: myining
* 创建时间: 2018/2/27 16:09:11
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using Shine.Core.Data;
using System;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class HostParameter_0x25_In:EntityBase<Guid>
    {
        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        /// 获取或设置 主机主控频段-网络号 
        /// </summary> 
        public int NetworkNumber { set; get; }

        /// <summary> 
        /// 获取或设置 主机系统容量-灯杆数（最大500） 
        /// </summary> 
        public int PoleNumber { set; get; }

        /// <summary> 
        /// 获取或设置 主控频段-频道号 
        /// </summary> 
        public int ChannelNumber { set; get; }

        /// <summary> 
        /// 获取或设置 系统容量-分组数量（最大16） 
        /// </summary> 
        public int GroupNumber { set; get; }

        /// <summary> 
        /// 获取或设置 系统语言（要注明是主机的）0：英文 1：中文 
        /// </summary> 
        public int SystemLanguage { set; get; }

        /// <summary> 
        /// 获取或设置 按键声音 0：关 1：开 
        /// </summary> 
        public int ButtonSound { set; get; }

        /// <summary> 
        /// 获取或设置 6位开机密码 
        /// </summary> 
        public string StartPassword { set; get; }

        /// <summary> 
        /// 获取或设置 主机网络选择 1:移动网络  2：网线  3：wifi 
        /// </summary> 
        public int NetworkSelection { set; get; }

        /// <summary> 
        /// 获取或设置 互感线圈-初级（1-100A） 
        /// </summary> 
        public int MutualInductanceLineOne { set; get; }

        /// <summary> 
        /// 获取或设置 互感线圈-次级（1-20mA） 
        /// </summary> 
        public int MutualInductanceLineTwo { set; get; }

        /// <summary>
        /// 主机信息上传更新间隔
        /// </summary>
        public int UpdateInterval { set; get; }

        /// <summary>
        /// 警报电压上限
        /// </summary>
        public int AlarmVoltageUpperLimit { set; get; }

        /// <summary>
        /// 警报电压下限
        /// </summary>
        public int AlarmVoltageLowerLimit { set; get; }
    }
}
