using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="DayElectricity"/>表数据实体映射
    /// </summary>
    public class DayElectricityConfiguration : EntityConfigurationBase<DayElectricity, Guid>
    {
        public DayElectricityConfiguration()
        {
            this.HasRequired(m => m.MonthElectricityOne)
                .WithMany(m => m.DayElectricityMany)
                .HasForeignKey(m => m.MonthElectricity_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
