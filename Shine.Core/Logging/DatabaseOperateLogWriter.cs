using Shine.Comman;
using Shine.Core.Data;
using System;

namespace Shine.Core.Logging
{
    /// <summary>
    /// 操作日志数据库输出实现
    /// </summary>
    public class DatabaseOperateLogWriter : IOperateLogWriter
    {
        private readonly IRepository<OperateLog, Guid> _operateLogRepository;

        /// <summary>
        /// 初始化一个<see cref="DatabaseOperateLogWriter"/>类型的新实例
        /// </summary>
        public DatabaseOperateLogWriter(IRepository<OperateLog, Guid> operateLogRepository)
        {
            _operateLogRepository = operateLogRepository;
        }

        /// <summary>
        /// 输出操作日志
        /// </summary>
        /// <param name="operateLog">操作日志信息</param>
        public void Write(OperateLog operateLog)
        {
            operateLog.CheckNotNull("operateLog");
            _operateLogRepository.Insert(operateLog);
        }
    }
}