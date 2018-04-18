using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class DataItemPageOutDto : IOutputDto
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

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置是否系统预设
        /// </summary>
        public bool IsSystem { set; get; }
    }
}