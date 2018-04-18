using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class GroupView:EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 分组编号
        /// </summary>
        public int GrounpNum { set; get; }

        /// <summary>
        /// 获取或设置分组名称
        /// </summary>
        public string GrounpName { set; get; }

        /// <summary>
        /// 获取或设置分组内容
        /// 存储形式用","隔开
        /// 如:1,1,1,1,1,1
        /// </summary>
        [MaxLength]
        public string GroupContent { set; get; }

        /// <summary>
        /// 获取或设置 该信息的说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// 获取或设置 分组信息更新的时间
        /// </summary>

        public DateTime UpdateTime { set; get; }

        /// <summary>
        /// 分组类型
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 分组类型名称
        /// </summary>

        public string DataItemDetailName { set; get; }


        /// <summary>
        /// 获取或设置 该主机所属的组织机构
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 该主机所属的组织机构的名称
        /// </summary>
        public string OrganizeName { set; get; }

        /// <summary>
        /// 分组对象
        /// </summary>
        public Guid? ObjectId { set; get; }
    }
}