using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.UserManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.UserManager
{
    /// <summary>
    /// <see cref="User"/>表数据实体映射
    /// </summary>
    public class UserConfiguration : EntityConfigurationBase<User, Guid>
    {
        public UserConfiguration()
        {
            this.HasRequired(m => m.UserLoginOne)
                .WithMany(m => m.UserMany)
                .HasForeignKey(m => m.UserLogin_Id)
                .WillCascadeOnDelete(true);
            
            this.Property(m => m.HeadIcon)
                .HasColumnType("image");
        }
    }
}
