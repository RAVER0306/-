using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class LightPoleView : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 灯杆编号
        /// </summary>
        public int PoleNum { set; get; }

        /// <summary>
        /// 获取或设置 灯杆的类型
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取灯杆类型名称
        /// </summary>

        public string DataItemDetailName { set; get; }

        /// <summary>
        /// 获取或设置 关联主机的主键
        /// </summary>

        public Guid Host_Id { set; get; }

        /// <summary>
        /// 获取或设置 主机名称
        /// </summary>
        public string HostName { set; get; }

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

        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreatedTime { set; get; }

        /// <summary>
        /// 获取或设置 该主机所属的组织机构
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 该主机所属的组织机构的名称
        /// </summary>
        public string OrganizeName { set; get; }

        /// <summary>
        /// 主机注册包
        /// </summary>
        public string RegPacket { set; get; }
    }
}