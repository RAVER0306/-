using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="HostPolicyLog"/> 表数据实体映射
    /// </summary>
    public class HostPolicyLogConfiguration:EntityConfigurationBase<HostPolicyLog,Guid>
    {
        public HostPolicyLogConfiguration()
        {
            this.HasRequired(m => m.HostOne)
                .WithMany(m => m.HostPolicyLogMany)
                .HasForeignKey(m => m.Host_Id);

            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.HasRequired(m => m.OrganizeOne)
                .WithMany(m => m.HostPolicyLogMany)
                .HasForeignKey(m => m.Organzie_Id);

            this.HasRequired(m => m.UserLoginOne)
                .WithMany()
                .HasForeignKey(m => m.UserLogin_Id);

            this.HasRequired(m => m.HostPolicyOne)
                .WithMany(m => m.HostPolicyLogMany)
                .HasForeignKey(m => m.HostPolicy_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
