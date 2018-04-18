using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.OrganzieManager.In
{
    public class GroupControlInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 分组对象主键
        /// </summary>
        public Guid? ObjectId { set; get; }

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

        ///// <summary>
        ///// 获取或设置 分组信息更新的时间
        ///// </summary>

        //public DateTime UpdateTime { set; get; }

        /// <summary>
        /// 获取或设置 分组类型的主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置所属机构
        /// </summary>
        public Guid Organzie_Id { set; get; }
                    
    }
}
