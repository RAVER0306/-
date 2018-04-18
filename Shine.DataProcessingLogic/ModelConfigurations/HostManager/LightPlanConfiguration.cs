using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="LightPlan"/>表数据实体映射
    /// </summary>
    public class LightPlanConfiguration : EntityConfigurationBase<LightPlan, Guid>
    {
        public LightPlanConfiguration()
        {
            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.HasRequired(m => m.HostOne)
                .WithMany(m => m.LightPlanMany)
                .HasForeignKey(m => m.Host_Id)
                .WillCascadeOnDelete(true);
                
        }
    }
}
