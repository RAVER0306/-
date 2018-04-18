using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="LightPole"/>表数据实体映射
    /// </summary>
    public class LightPoleConfiguration:EntityConfigurationBase<LightPole,Guid>
    {
        public LightPoleConfiguration()
        {
            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.HasMany(m => m.SubControlMany)
                .WithRequired(m => m.LigthPoleOne)
                .HasForeignKey(m=>m.LigthPoleOne_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
