using Shine.DataProcessingLogic.Base.OrgizeManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Models.OrganizeManager
{
    /// <summary>
    /// 年度电量统计实体基类
    /// </summary>
    [Description("年度电量统计实体基类")]
    public class AnnualElectricity : AnnualElectricityBase<Guid>
    {
       public AnnualElectricity()
        {
            MonthElectricityMany = new HashSet<MonthElectricity>();
        }

        /// <summary>
        /// 获取或设置 关联的 <see cref="DataItemDetail"/> 类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 关联的 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetail DataItemDetailOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organzie_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }

        /// <summary>
        /// 获取或设置 关联对象主键
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// 获取或设置 <see cref="MonthElectricity"/>实体对象集合
        /// </summary>
        public virtual ICollection<MonthElectricity> MonthElectricityMany { set; get; }
    }
}
