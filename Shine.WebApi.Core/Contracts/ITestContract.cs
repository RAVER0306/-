using Shine.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.WebApi.Core.Contracts
{
    public interface ITestContract : IScopeDependency
    {
       void Test();
    }
}
