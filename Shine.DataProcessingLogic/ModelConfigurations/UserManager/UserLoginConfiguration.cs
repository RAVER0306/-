using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.UserManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.UserManager
{
    /// <summary>
    /// <see cref="UserExtend"/>表数据实体映射
    /// </summary>
    public class UserLoginConfiguration : EntityConfigurationBase<UserLogin, Guid>
    {
        public UserLoginConfiguration()
        {
            this.HasRequired(m => m.OrganizeOne)
                .WithMany(m => m.UserLoginMany)
                .HasForeignKey(m => m.Organize_Id)
                .WillCascadeOnDelete(true);

            this.HasIndex(m => m.UserName).IsUnique(true);

            this.Property(m => m.FirstVisitTime).HasColumnType("datetime2");
            this.Property(m => m.LastVisitTime).HasColumnType("datetime2");
            this.Property(m => m.PreviousVisitTime).HasColumnType("datetime2");
            this.Property(m => m.LockoutEndDateUtc).HasColumnType("datetime2");
        }
    }
}
