using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="HostParameter"/>表数据实体映射
    /// </summary>
    public class HostParameterConfiguration:EntityConfigurationBase<HostParameter,Guid>
    {
        public HostParameterConfiguration()
        {
            this.HasRequired(m => m.HostOne)
                .WithMany(m => m.HostParameterMany)
                .HasForeignKey(m => m.Host_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
