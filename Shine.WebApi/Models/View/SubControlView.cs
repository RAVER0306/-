using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class SubControlView : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 分控编号
        /// </summary>
        public int SubNum { set; get; }

        /// <summary>
        /// 获取或设置 分控名称
        /// </summary>
        [StringLength(64)]
        public string SubName { set; get; }

        /// <summary>
        /// 获取或设置 分控UID
        /// </summary>
        [StringLength(64)]
        public string UID { set; get; }

        /// <summary>
        /// 获取或设置 分控信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 分控类型的主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取分控类型
        /// </summary>
        public string DataItemDetailName { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>关联主键
        /// </summary>
        public Guid LigthPoleOne_Id { set; get; }

        /// <summary>
        /// 获取分控关联的灯杆名称
        /// </summary>
        public string LigthPoleOneName { set; get; }

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

        /// <summary> 
        ///  获取或设置 分控位置经度
        /// </summary> 
        public double Longitude { set; get; }

        /// <summary> 
        ///  获取或设置 分控位置纬度
        /// </summary> 
        public double Latitude { set; get; }
    }
}