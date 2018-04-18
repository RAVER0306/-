using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.OrganzieManager.Out
{
    public class DataItemDetailOutDto : IOutputDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 字典目录名称
        /// </summary>
        [StringLength(64)]
        public string FullName { set; get; }

        /// <summary>
        /// 获取或设置 字典目录的查询编码
        /// </summary>
        [StringLength(64)]
        public string QueryCoding { set; get; }

        /// <summary>
        /// 获取或设置 该信息的描述
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 该数据的序号(用作多语言判断)
        /// </summary>
        [Range(0, 255)]
        public byte Index { set; get; }
    }
}
