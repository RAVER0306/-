using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Dtos.OrganzieManager.In
{
    public class DataItemInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 数据主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 该字典是否系统预设的公共字典目录
        /// </summary>
        [DefaultValue(false)]
        public bool IsPublic { set; get; }

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
    }
}
