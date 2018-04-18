using Shine.Core.Dependency;
using Shine.Core.Reflection;

namespace Shine.Core.Security
{
    /// <summary>
    /// 定义实体类型查找
    /// </summary>
    public interface IEntityTypeFinder : ITypeFinder, ISingletonDependency
    { }
}