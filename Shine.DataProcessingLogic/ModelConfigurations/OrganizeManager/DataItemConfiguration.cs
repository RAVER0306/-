using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="DataItem"/>表数据实体映射
    /// </summary>
    public class DataItemConfiguration : EntityConfigurationBase<DataItem, Guid>
    {
        public DataItemConfiguration()
        {
            this.HasOptional(m => m.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(m => m.ParentId);
        }
    }
}
