using Shine.Core.Data;
using System;
using System.Collections.Generic;

namespace Shine.WebApi.Models
{
    public class InformationView : EntityBase<Guid>, IOutputDto
    {
        /// <summary>
        /// 获取或设置 创建该数据的时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取 提示信息类型名称
        /// </summary>
        public string TypeName { set; get; }

        /// <summary>
        /// 获取 提示信息类型标识
        /// </summary>
        public int TypeIndex { get; set; }

        /// <summary>
        /// 获取或设置 用户信息是否已经读取
        /// </summary>
        public bool IsReaded { set; get; }

        /// <summary>
        /// 获取或设置 信息类型目标主键
        /// </summary>
        public Guid ObjectId { set; get; }

        ///// <summary>
        ///// 获取或设置 信息类型目标结果
        ///// </summary>
        //public dynamic ObjectResult
        //{
        //    set; get;
        //}

    }
}
