using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class LightPoleInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 灯杆编号
        /// </summary>
        public int PoleNum { set; get; }

        /// <summary>
        /// 获取或设置 灯杆的类型
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 关联主机的主键
        /// </summary>

        public Guid Host_Id { set; get; }

        /// <summary>
        /// 获取或设置灯杆名称
        /// </summary>
        [StringLength(64)]
        public string PoleName { set; get; }

        /// <summary>
        /// 获取或设置 灯杆信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 灯杆的地址
        /// </summary>

        [StringLength(128)]
        public string Address { set; get; }
    }
}
