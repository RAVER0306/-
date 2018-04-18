/*===================================================
* 类名称: Host_0x61_In
* 类描述:
* 创建人: 72440
* 创建时间: 2018/3/12 14:37:41
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
    public class Host_0x61_In:EntityBase<Guid>
    {
        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        ///  主机位置经度
        /// </summary> 
        public double Longitude { set; get; }

        /// <summary> 
        ///  主机位置纬度
        /// </summary> 
        public double Latitude { set; get; }

        /// <summary> 
        ///  主机当前的所属时区
        /// </summary> 
        public int TimeZone { set; get; }
    }
}
