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
    /// 天电量统计实体
    /// </summary>
    [Description("天电量统计实体")]
    public class DayElectricity : DayElectricityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 <see cref="MonthElectricity"/> 对象实体
        /// </summary>
        public virtual MonthElectricity MonthElectricityOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="MonthElectricity"/> 对象实体主键
        /// </summary>
        public Guid MonthElectricity_Id { set; get; }
    }
}
