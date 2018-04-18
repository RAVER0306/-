/*===================================================
* 类名称: GroupControl_0x18_In
* 类描述: 用来接收分组数据输入的实体
* 创建人: myining
* 创建时间: 2018/2/26 10:35:06
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

namespace Shine.DataProcessingLogic.Dtos.OrganzieManager.In
{
    public class GroupControl_0x18_In : EntityBase<Guid>
    {
        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary>
        /// 获取或设置 分组编号
        /// </summary>
        public int GrounpNum { set; get; }

        /// <summary>
        /// 获取或设置分组内容
        /// 存储形式用","隔开
        /// 如:1,1,1,1,1,1
        /// </summary>
        public string GroupContent { set; get; }
    }
}
