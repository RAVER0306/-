using Shine.Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Dtos.UserManager
{
    public class UserBlackListOutDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 黑名单内容
        /// </summary>
        [MaxLength]
        public string BlackList { set; get; }

        /// <summary>
        /// 获取或设置 创建该数据的时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取 黑名单类型名称
        /// </summary>
        public string TypeName { set; get; }

        /// <summary>
        /// 获取 黑名单类型标识
        /// </summary>
        public int TypeIndex { get; set; }
    }
}
