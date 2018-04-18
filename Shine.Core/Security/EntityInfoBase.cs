using Shine.Comman.Extensions;
using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shine.Core.Security
{
    /// <summary>
    /// 实体信息基类
    /// </summary>
    public abstract class EntityInfoBase<TKey> : EntityBase<TKey>, IEntityInfo
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 实体类型全名
        /// </summary>
        [Required, DisplayName("类型全名")]
        public string ClassName { get; set; }

        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        [Required, DisplayName("实体名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否启用数据日志
        /// </summary>
        [DisplayName("是否启用数据日志")]
        public bool DataLogEnabled { get; set; }

        /// <summary>
        /// 获取或设置 实体属性信息Json字符串
        /// </summary>
        public string PropertyNamesJson { get; set; }

        /// <summary>
        /// 获取 实体属性信息字典
        /// </summary>
        [NotMapped]
        public IDictionary<string, string> PropertyNames
        {
            get
            {
                if (PropertyNamesJson.IsNullOrEmpty())
                {
                    return new Dictionary<string, string>();
                }
                return PropertyNamesJson.FromJsonString<Dictionary<string, string>>();
            }
        }

        /// <summary>
        /// 获取或设置 是否已逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}