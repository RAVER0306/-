using Shine.Core.Data;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.UserManager
{
    public class Information : EntityBase<Guid>, ICreatedTime
    {
        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 表数据实体主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 表数据实体
        /// </summary>
        public virtual DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取 提示信息类型名称
        /// </summary>
        [StringLength(64)]
        public string TypeName { set; get; }

        /// <summary>
        /// 获取 提示信息类型标识
        /// </summary>
        public int TypeIndex { set; get; } 

        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 实体对象主键
        /// </summary>
        public Guid UserLogin_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 实体对象
        /// </summary>
        public UserLogin UserLoginOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/> 实体信息主键
        /// </summary>
        public Guid Organize_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/> 实体信息 
        /// </summary>
        public Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 用户信息是否已经读取
        /// </summary>
        public bool IsReaded { set; get; }

        /// <summary>
        /// 获取或设置 该信息创建的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }

        /// <summary>
        /// 获取或设置 信息类型目标主键
        /// </summary>
        public Guid ObjectId { set; get; }
    }
}
