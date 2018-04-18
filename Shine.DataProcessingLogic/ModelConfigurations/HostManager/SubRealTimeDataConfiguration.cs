using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="SubRealTimeData"/>表数据实体映射
    /// </summary>
    public class SubRealTimeDataConfiguration:EntityConfigurationBase<SubRealTimeData,Guid>
    {
        public SubRealTimeDataConfiguration()
        {
            this.HasRequired(m => m.SubControlOne)
                .WithMany(m => m.SubRealTimeDataMany)
                .HasForeignKey(m => m.SubControl_Id)
                .WillCascadeOnDelete(true);

            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);
        }
    }
}
