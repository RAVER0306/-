using Shine.Core.Data;
using Shine.Core.Logging;
using Shine.DataProcessingLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Services
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
    }
}
