using Shine.Core.Data;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.Out;
using System;

namespace Shine.WebApi.Models.OutDtos
{
    public class UpgradePackagesOutDto : EntityBase<Guid>, IOutputDto
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 组织机构的关联 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetailOutDto DataItemDetailOne { set; get; }

        /// <summary>
        /// 包名称
        /// </summary>

        public string PackName { set; get; }

        /// <summary>
        /// 包版本
        /// </summary>
        public string Version { set; get; }
    }
}