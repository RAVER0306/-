/*===================================================
* 类名称: SubRealTimeData_0x16_In
* 类描述: 用于更新分控实时数据映射的实体
* 创建人: myining
* 创建时间: 2018/1/29 20:45:07
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
    public class SubRealTimeData_0x16_In : EntityBase<Guid>
    {
        /// <summary> 
        ///  分控调光端口
        /// </summary> 
        public int DimmingPort { set; get; }

        /// <summary> 
        ///  分控端口上的灯具亮度
        /// </summary> 
        public int Brightness { set; get; }

        /// <summary> 
        ///  分控的电压
        /// </summary> 
        public double Voltage { set; get; }

        /// <summary> 
        ///  分控的电流
        /// </summary> 
        public double Current { set; get; }

        /// <summary> 
        ///  分控的功率
        /// </summary> 
        public double Power { set; get; }

        /// <summary> 
        ///  分控的温度
        /// </summary> 
        public int Temperature { set; get; }

        /// <summary> 
        ///  频率
        /// </summary> 
        public int Frequency { set; get; }

        /// <summary> 
        ///  更新时间
        /// </summary> 
        public DateTime UpdateTime { set; get; }

        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary>
        /// 分控（节点编号）
        /// </summary>

        public int SubNum { set; get; }

        /// <summary>
        /// 能耗信息
        /// </summary>
        public double EnergyConsumption { set; get; }
    }
}
