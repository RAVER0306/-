using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.UserManager.In
{
    public class UserBlackListInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 获取或设置 主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 黑名单名称
        /// </summary>
        public string FullName { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 表数据实体主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 黑名单内容
        /// </summary>
        public string BlackList { set; get; }
    }
}
