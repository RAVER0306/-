/*===================================================
* 类名称: HostRealTimeDataInDto
* 类描述: 更新主机实时数据的映射输入表
* 创建人: myining
* 创建时间: 2018/1/29 16:38:24
* 修改人: 
* 修改时间:
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
    public class HostRealTimeDataInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        ///  主机当前的回路状态
        /// </summary> 
        public string LoopState { set; get; }

        /// <summary> 
        ///  主机当前三相电电压
        /// </summary> 
        public string Voltage { set; get; }

        /// <summary> 
        ///  主机三相电电流
        /// </summary> 
        public string Current { set; get; }

        /// <summary> 
        ///  主机三相电功率
        /// </summary> 
        public string Power { set; get; }

        /// <summary> 
        ///  主机温度
        /// </summary> 
        public int Temperature { set; get; }

        /// <summary> 
        ///  主机位置经度
        /// </summary> 
        //public double Longitude { set; get; }

        ///// <summary> 
        /////  主机位置纬度
        ///// </summary> 
        //public double Latitude { set; get; }

        /// <summary> 
        ///  主机当前的所属时区
        /// </summary> 
        //public int TimeZone { set; get; }

        /// <summary>
        /// 主机基础信息更新时间
        /// </summary>

        public DateTime UpdateTime { set; get; }

        /// <summary>
        /// 能耗信息
        /// </summary>
        public double EnergyConsumption { set; get; }
    }
}
