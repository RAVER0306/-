using Shine.DataProcessingLogic.Base.UserManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.UserManager
{
    /// <summary>
    /// 用户黑名单信息
    /// </summary>
    [Description("用户黑名单信息")]
    public class UserBlackList : UserBlackListBase<Guid>
    {
        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 表数据实体主键
        /// </summary>
        public Guid UserLogin_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 表数据实体
        /// </summary>
        public virtual UserLogin UserLoginOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 表数据实体主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 表数据实体
        /// </summary>
        public virtual DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取 黑名单类型名称
        /// </summary>
        [NotMapped]
        public string TypeName { get { return DataItemDetailOne.FullName; } }

        /// <summary>
        /// 获取 黑名单标识
        /// </summary>
        [NotMapped]
        public int TypeIndex { get { return DataItemDetailOne.Index; } }
    }
}
