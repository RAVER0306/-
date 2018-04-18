using Shine.Comman.Extensions;
using Shine.Core.Data;
using Shine.Core.Mapping;
using Shine.Core.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace Shine.Core.Security
{
    /// <summary>
    /// 实体类型查找器
    /// </summary>
    public class EntityTypeFinder : IEntityTypeFinder, IMapSourceTypeFinder, IMapTargetTypeFinder
    {
        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            Assembly[] assemblies = AssemblyFinder.FindAll();
            return assemblies.SelectMany(assembly =>
                assembly.GetTypes().Where(type =>
                    typeof(IEntity<>).IsGenericAssignableFrom(type) && !type.IsAbstract))
                .Distinct().ToArray();
        }
    }
}