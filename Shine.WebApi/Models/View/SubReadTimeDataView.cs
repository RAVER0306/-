using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class SubReadTimeDataView:EntityBase<Guid>
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
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>实体对象主键
        /// </summary>
        public Guid SubControl_Id { set; get; }

        /// <summary>
        /// 获取或设置 关联分控的名称
        /// </summary>
        public string SubControlName { set; get; }

        /// <summary>
        /// 获取或设置 在<see cref="DataItemDetail"/>中的灯具类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 关联灯具的类型名称
        /// </summary>
        public string DataItemDetailName { set; get; }

        /// <summary>
        /// 主机注册包
        /// </summary>
        public string RegPacket { set; get; }
    }
}