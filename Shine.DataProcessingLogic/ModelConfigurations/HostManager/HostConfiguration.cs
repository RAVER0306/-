using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="HostConfiguration"/>表数据实体映射
    /// </summary>
    public class HostConfiguration:EntityConfigurationBase<Host,Guid>
    {
        public HostConfiguration()
        {
            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.HasIndex(m => m.RegPackage)
                .IsUnique(true);

            this.HasRequired(m => m.OrganizeOne)
                .WithMany(m => m.HostMany)
                .HasForeignKey(m => m.Organize_Id)
                .WillCascadeOnDelete(true);

            this.HasMany(m => m.LightPoleMany)
                .WithRequired(m=>m.HostOne)
                .HasForeignKey(m=>m.Host_Id)
                .WillCascadeOnDelete(true);

            this.HasMany(m => m.HostPolicyMany)
                .WithRequired(m=>m.HostOne)
                .HasForeignKey(m=>m.Host_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
