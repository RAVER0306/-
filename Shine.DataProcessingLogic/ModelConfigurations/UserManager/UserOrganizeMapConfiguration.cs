using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.UserManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.UserManager
{
    /// <summary>
    /// <see cref="UserOrganizeMap"/>表数据实体映射
    /// </summary>
    public class UserOrganizeMapConfiguration : EntityConfigurationBase<UserOrganizeMap, Guid>
    {
        public UserOrganizeMapConfiguration()
        {
            this.HasRequired(m => m.UserLoginOne)
                .WithMany(m => m.UserOrganizeMapMany)
                .HasForeignKey(m=>m.UserLogin_Id)
                .WillCascadeOnDelete(true);

            this.HasRequired(m => m.OrganizeOne)
                .WithMany()
                .HasForeignKey(m=>m.Organize_Id);
        }
    }
}
