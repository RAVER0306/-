using Shine.Core.Data;
using Shine.DataProcessingLogic.Models.HostManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.OrganizeManager
{
    public class SubAggregation: EntityBase<Guid>, ICreatedTime
    {
        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organzie_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体
        /// </summary>
        public virtual Host HostOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体主键
        /// </summary>
        public Guid Host_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>表数据实体
        /// </summary>
        public LightPole LightPoleOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>表数据实体主键
        /// </summary>
        public Guid LightPole_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>实体对象
        /// </summary>
        public virtual SubControl SubControlOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>实体对象主键
        /// </summary>
        public Guid SubControl_Id { set; get; }

        /// <summary>
        /// 获取或设置 该信息创建的时间
        /// </summary>
        public DateTime CreatedTime { set; get; }
    }
}
