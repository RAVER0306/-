﻿using Shine.Comman.Data;
using Shine.Core.Dependency;
using Shine.Core.Logging;
using System;
using System.Linq;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface ILoggingContract : IScopeDependency
    {
        #region 数据日志信息业务

        /// <summary>
        /// 获取 操作日志信息查询数据集
        /// </summary>
        IQueryable<OperateLog> OperateLogs { get; }

        /// <summary>
        /// 获取 数据日志信息查询数据集
        /// </summary>
        IQueryable<DataLog> DataLogs { get; }

        /// <summary>
        /// 获取 数据日志项信息查询数据集
        /// </summary>
        IQueryable<DataLogItem> DataLogItems { get; }

        /// <summary>
        /// 删除操作日志信息信息
        /// </summary>
        /// <param name="ids">要删除的操作日志信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteOperateLogs(params Guid[] ids);

        #endregion

    }
}
