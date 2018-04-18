using Shine.DataProcessingLogic.Base.OrgizeManager.Models;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.OrganizeManager
{
    /// <summary>
    /// 组织机构表实体
    /// </summary>
    [Description("组织机构基本信息")]
    public class Organize:OrganizeBase<Guid>
    {
        public Organize()
        {
            Children = new HashSet<Organize>();
            DataItemDetailMany = new HashSet<DataItemDetail>();
            UserLoginMany = new HashSet<UserLogin>();
            HostMany = new HashSet<Host>();
            HostPolicyMany = new HashSet<HostPolicy>();
            HostPolicyLogMany = new HashSet<HostPolicyLog>();
            GroupControlMany = new HashSet<GroupControl>();
            SubAggregationMany = new HashSet<SubAggregation>();
            AnnualElectricityMany=new HashSet<AnnualElectricity>();
        }

        ///// <summary>
        ///// 获取或设置 该组织下的所有<see cref="UserLogin"/>用户
        ///// </summary>

        public virtual ICollection<UserLogin> UserLoginMany { set; get; }


        /// <summary>
        /// 获取或设置 组织机构的关联 <see cref="DataItemDetail"/> 类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 组织机构的关联 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 实体集合
        /// </summary>
        public virtual ICollection<DataItemDetail> DataItemDetailMany { set; get; }

        /// <summary>
        /// 获取或设置 父级组织机构信息
        /// </summary>
        public virtual Organize Parent { get; set; }

        /// <summary>
        /// 获取或设置 子级组织机构信息集合
        /// </summary>
        public virtual ICollection<Organize> Children { get; set; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<Host> HostMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicy"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<HostPolicy> HostPolicyMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicyLog"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<HostPolicyLog> HostPolicyLogMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="GroupControl"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<GroupControl> GroupControlMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubAggregation"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<SubAggregation> SubAggregationMany { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="AnnualElectricity"/>表数据库实体集合
        /// </summary>
        public virtual ICollection<AnnualElectricity> AnnualElectricityMany { set; get; }
    }
}
