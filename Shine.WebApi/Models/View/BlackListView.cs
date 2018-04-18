using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class BlackListView: EntityBase<Guid>
    {
        /// <summary>
        /// 建立该黑名单的用户
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 该黑名单所属的类型
        /// </summary>
        public string DataItemDetailName { set; get; }

        /// <summary>
        /// 该黑名单所属的类型序号
        /// </summary>
        public byte DataItemDetailIndex { set; get; }

        /// <summary>
        /// 黑名单内容
        /// </summary>
        public string BlackList { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { set; get; }

        /// <summary>
        /// 该黑名单的名称
        /// </summary>
        public string FullName { set; get; }
    }
}