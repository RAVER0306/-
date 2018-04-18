using System;
using System.Collections.Concurrent;

namespace Shine.Comman.HPSocket
{
    /// <summary>
    /// 附加数据
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class Extra<T>
    {
        ConcurrentDictionary<IntPtr, T> dict = new ConcurrentDictionary<IntPtr, T>();

        /// <summary>
        /// 获取附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(IntPtr key)
        {
            if (dict.TryGetValue(key, out T value))
            {
                return value;
            }
            return default(T);
        }

        /// <summary>
        /// 设置附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(IntPtr key, T newValue)
        {
            try
            {
                dict.AddOrUpdate(key, newValue, (tKey, existingVal) => { return newValue; });
                return true;
            }
            catch (OverflowException)
            {
                // 字典数目超过int.max
                return false;
            }
            catch (ArgumentNullException)
            {
                // 参数为空
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(IntPtr key) => dict.TryRemove(key, out T value);
    }

    /// <summary>
    /// 连接时附加数据
    /// </summary>
    public class ConnectionExtra
    {
        ConcurrentDictionary<IntPtr, object> dict = new ConcurrentDictionary<IntPtr, object>();

        /// <summary>
        /// 获取附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetExtra(IntPtr key)
        {
            if (dict.TryGetValue(key, out object value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// 获取附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetExtra<T>(IntPtr key)
        {
            if (dict.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default(T);
        }

        /// <summary>
        /// 设置附加数据
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetExtra(IntPtr key, object newValue)
        {
            try
            {
                dict.AddOrUpdate(key, newValue, (tKey, existingVal) => { return newValue; });
                return true;
            }
            catch (OverflowException)
            {
                // 字典数目超过int.max
                return false;
            }
            catch (ArgumentNullException)
            {
                // 参数为空
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveExtra(IntPtr key)
        {
            return dict.TryRemove(key, out object value);
        }
    }
}
