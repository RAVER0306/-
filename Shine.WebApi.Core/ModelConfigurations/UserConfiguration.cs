using Shine.Data.EF;
using Shine.WebApi.Core.Models;
using System;

namespace Shine.WebApi.Core.ModelConfigurations
{
    public class UserConfiguration:EntityConfigurationBase<User, Guid>
    {}
}
