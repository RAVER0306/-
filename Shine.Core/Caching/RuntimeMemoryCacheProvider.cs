
using System.Collections.Concurrent;

namespace Shine.Core.Caching
{
    /// <summary>
    /// 运行时内存缓存提供程序
    /// </summary>
    public class RuntimeMemoryCacheProvider : ICacheProvider
    {
        private static readonly ConcurrentDictionary<string, ICache> Caches;

        static RuntimeMemoryCacheProvider()
        {
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        /// <summary>
        /// 获取 缓存是否可用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="regionName">缓存区域名称</param>
        /// <returns></returns>
        public ICache GetCache(string regionName)
        {
            if (Caches.TryGetValue(regionName, out ICache cache))
            {
                return cache;
            }
            cache = new RuntimeMemoryCache(regionName);
            Caches[regionName] = cache;
            return cache;
        }

    }
}