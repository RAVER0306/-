using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="HostLogin"/>表数据实体映射
    /// </summary>
    public class HostLoginConfiguration:EntityConfigurationBase<HostLogin,Guid>
    {
        public HostLoginConfiguration()
        {
            this.HasRequired(m => m.HostOne)
                .WithMany(m => m.HostLoginMany)
                .HasForeignKey(m => m.Host_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
