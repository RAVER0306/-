using Shine.Core.Data;
using Shine.Core.Logging;
using Shine.WebApi.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shine.Comman.Data;

namespace Shine.WebApi.Core.Services
{
    public partial class LoggingService : ILoggingContract
    {
        /// <summary>
        /// 获取或设置 操作日志仓储对象
        /// </summary>
        public IRepository<OperateLog, Guid> OperateLogRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 数据日志仓储对象
        /// </summary>
        public IRepository<DataLog, Guid> DataLogRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 数据日志项仓储对象
        /// </summary>
        public IRepository<DataLogItem, Guid> DataLogItemRepository { protected get; set; }

        #region Implementation of ILoggingContract

        /// <summary>
        /// 获取 操作日志信息查询数据集
        /// </summary>
        public IQueryable<OperateLog> OperateLogs
        {
            get { return OperateLogRepository.Entities; }
        }

        /// <summary>
        /// 获取 数据日志信息查询数据集
        /// </summary>
        public IQueryable<DataLog> DataLogs
        {
            get { return DataLogRepository.Entities; }
        }

        /// <summary>
        /// 获取 数据日志项信息查询数据集
        /// </summary>
        public IQueryable<DataLogItem> DataLogItems
        {
            get { return DataLogItemRepository.Entities; }
        }

        /// <summary>
        /// 删除操作日志信息信息
        /// </summary>
        /// <param name="ids">要删除的操作日志信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteOperateLogs(params Guid[] ids)
        {
            return OperateLogRepository.Delete(ids);
        }

        #endregion
    }
}
 