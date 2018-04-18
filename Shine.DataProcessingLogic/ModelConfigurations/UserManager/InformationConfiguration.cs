using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.UserManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.UserManager
{
    /// <summary>
    /// <see cref="Information"/>表数据实体映射
    /// </summary>
    public class InformationConfiguration:EntityConfigurationBase<Information,Guid>
    {
        public InformationConfiguration()
        {
            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.HasRequired(m => m.OrganizeOne)
                .WithMany()
                .HasForeignKey(m => m.Organize_Id);

            this.HasRequired(m => m.UserLoginOne)
                .WithMany(m => m.InformationMany)
                .HasForeignKey(m => m.UserLogin_Id)
                .WillCascadeOnDelete(true);

        }
    }
}
