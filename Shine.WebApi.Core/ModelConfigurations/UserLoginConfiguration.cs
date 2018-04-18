using Shine.Data.EF;
using Shine.WebApi.Core.Models;
using System;

namespace Shine.WebApi.Core.ModelConfigurations
{
    public class UserLoginConfiguration:EntityConfigurationBase<UserLogin, Guid>
    {
        public UserLoginConfiguration()
        {
            this.HasMany(m => m.UserMany)
                .WithRequired(m => m.UserLoginOne)
                //.HasForeignKey(m=>m.UserLogin_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
