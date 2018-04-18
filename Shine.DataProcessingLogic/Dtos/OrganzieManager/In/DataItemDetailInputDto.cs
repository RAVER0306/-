using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.OrganzieManager.In
{
    public class DataItemDetailInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 所属的字典目录
        /// </summary>
        public Guid DataItem_Id { set; get; }

        /// <summary>
        /// 字典内容的名称
        /// </summary>
        public string FullName { set; get; }

        /// <summary>
        /// 是否被锁定
        /// </summary>
        public bool IsLocked { set; get; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { set; get; }

        /// <summary>
        /// 该项的说明
        /// </summary>
        public string Remark { set; get; }
    }
}
