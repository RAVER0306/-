using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="DataItemDetail"/>表数据实体映射
    /// </summary>
    public class DataItemDetailConfiguration : EntityConfigurationBase<DataItemDetail, Guid>
    {
        public DataItemDetailConfiguration()
        {
            this.HasOptional(m => m.OrganizeOne)
                .WithMany(m => m.DataItemDetailMany)
                .HasForeignKey(m => m.Organzie_Id)
                .WillCascadeOnDelete(true);

            this.HasRequired(m => m.DataItemOne)
                .WithMany(m => m.DataItemDetailMany)
                .HasForeignKey(m => m.DataItem_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
