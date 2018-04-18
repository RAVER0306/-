using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="MonthElectricity"/>表数据实体映射
    /// </summary>
    public class MonthElectricityConfiguration : EntityConfigurationBase<MonthElectricity, Guid>
    {
        public MonthElectricityConfiguration()
        {
            this.HasRequired(m => m.AnnualElectricityOne)
                .WithMany(m => m.MonthElectricityMany)
                .HasForeignKey(m => m.AnnualElectricity_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
