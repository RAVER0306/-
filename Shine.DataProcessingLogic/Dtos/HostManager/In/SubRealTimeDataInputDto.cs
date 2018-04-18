using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class SubRealTimeDataInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>实体对象主键
        /// </summary>
        public Guid SubControl_Id { set; get; }

        /// <summary>
        /// 获取或设置 在<see cref="DataItemDetail"/>中的灯具类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary> 
        /// 获取或设置 灯具在绑定分控上的调光端口 
        /// </summary> 
        public int DimmingPort { set; get; }

        /// <summary>
        /// 获取或设置 备注名
        /// </summary>
        [StringLength(64)]
        public string FullName { set; get; }
    }
}
