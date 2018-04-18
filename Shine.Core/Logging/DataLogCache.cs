using System.Collections.Generic;

namespace Shine.Core.Logging
{
    /// <summary>
    /// 数据日志缓存类
    /// </summary>
    public class DataLogCache : IDataLogCache
    {
        private readonly IList<DataLog> _dataLogs;

        /// <summary>
        /// 初始化一个<see cref="DataLogCache"/>类型的新实例
        /// </summary>
        public DataLogCache()
        {
            _dataLogs = new List<DataLog>();
        }

        /// <summary>
        /// 获取 数据日志集合
        /// </summary>
        public IEnumerable<DataLog> DataLogs
        {
            get { return _dataLogs; }
        }

        /// <summary>
        /// 向缓存中添加数据日志信息
        /// </summary>
        /// <param name="dataLog">数据日志信息</param>
        public void AddDataLog(DataLog dataLog)
        {
            _dataLogs.Add(dataLog);
        }
    }
}