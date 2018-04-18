using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="HostPolicy"/>表数据实体映射
    /// </summary>
    public class HostPolicyConfiguration:EntityConfigurationBase<HostPolicy,Guid>
    {
        public HostPolicyConfiguration()
        {
            this.HasRequired(m => m.OrganizeOne)
                .WithMany(m => m.HostPolicyMany)
                .HasForeignKey(m => m.Organzie_Id);
        }
    }
}
