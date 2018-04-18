using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.ModelConfigurations.UserManager
{
    /// <summary>
    /// <see cref="UserBlackList"/>表数据实体映射
    /// </summary>
    public class UserBlackListConfiguration:EntityConfigurationBase<UserBlackList,Guid>
    {
        public UserBlackListConfiguration()
        {
            this.HasRequired(m => m.UserLoginOne)
                .WithMany(m => m.UserBlackListMany)
                .HasForeignKey(m => m.UserLogin_Id)
                .WillCascadeOnDelete(true);

            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);
        }
    }
}
