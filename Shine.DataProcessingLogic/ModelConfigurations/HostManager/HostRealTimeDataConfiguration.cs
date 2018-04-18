using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="HostRealTimeData"/>表数据实体映射
    /// </summary>
    public class HostRealTimeDataConfiguration : EntityConfigurationBase<HostRealTimeData, Guid>
    {
        public HostRealTimeDataConfiguration()
        {           
            this.HasRequired(m => m.HostOne)
                .WithMany(m => m.HostRealTimeDataMany)
                .HasForeignKey(m => m.Host_Id)
                .WillCascadeOnDelete(true);

            this.Property(m => m.Timestamp)
                .IsRowVersion();
        }
    }
}
