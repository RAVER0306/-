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
    /// 月度电量统计实体
    /// </summary>
    [Description("月度电量统计实体")]
    public class MonthElectricity : MonthElectricityBase<Guid>
    {
        public MonthElectricity()
        {
            DayElectricityMany = new HashSet<DayElectricity>();
        }

        /// <summary>
        /// 获取或设置 <see cref="AnnualElectricity"/>实体对象
        /// </summary>
        public virtual AnnualElectricity AnnualElectricityOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="AnnualElectricity"/>实体对象主键
        /// </summary>
        public Guid AnnualElectricity_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DayElectricity"/>实体对象集合
        /// </summary>

        public virtual ICollection<DayElectricity> DayElectricityMany { set; get;}
    }
}
