using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="GroupControl"/>表数据实体映射
    /// </summary>
    public class GroupControlConfiguration:EntityConfigurationBase<GroupControl,Guid>
    {
        public GroupControlConfiguration()
        {
            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.HasRequired(m => m.OrganizeOne)
                .WithMany(m => m.GroupControlMany)
                .HasForeignKey(m => m.Organzie_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
