using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.OrganizeManager
{
    /// <summary>
    /// 升级包实体管理类
    /// </summary>
    public class UpgradePackages : EntityBase<Guid>, ICreatedTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 升级包的 <see cref="DataItemDetail"/> 类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 升级包的 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 升级包数据
        /// </summary>
        public byte[] Datas { set; get; }

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
