using Shine.Core.Security;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.HostManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.HostManager
{
    /// <summary>
    /// <see cref="SubControl"/>表数据实体映射
    /// </summary>
    public class SubControlConfiguration:EntityConfigurationBase<SubControl,Guid>
    {
        public SubControlConfiguration()
        {
            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);
        }
    }
}
