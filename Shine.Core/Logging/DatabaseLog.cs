﻿using Shine.Comman.Logging;
using Shine.Core.Configs;
using Shine.Core.Dependency;
using System;
using System.Collections.Generic;

namespace Shine.Core.Logging
{
    /// <summary>
    /// 数据库日志输出者
    /// </summary>
    public class DatabaseLog : LogBase, IScopeDependency
    {
        private static LogLevel? _outLogLevel;

        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取 是否数据日志对象
        /// </summary>
        public override bool IsDataLogging
        {
            get { return true; }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        public override bool IsTraceEnabled
        {
            get { return IsLevelEnabled(LogLevel.Trace); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return IsLevelEnabled(LogLevel.Debug); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return IsLevelEnabled(LogLevel.Info); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return IsLevelEnabled(LogLevel.Warn); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return IsLevelEnabled(LogLevel.Error); }
        }

        /// <summary>
        /// 获取 是否允许输出<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return IsLevelEnabled(LogLevel.Fatal); }
        }

        /// <summary>
        /// 是否启用日志输出级别
        /// </summary>
        /// <param name="level">日志输出</param>
        /// <returns>日志输出级别</returns>
        private static bool IsLevelEnabled(LogLevel level)
        {
            if (_outLogLevel == null)
            {
                ShineConfig config = ShineConfig.Instance;
                _outLogLevel = config.LoggingConfig.DataLoggingConfig.OutLevel;
            }
            return level >= _outLogLevel;
        }

        /// <summary>
        /// 获取日志输出处理委托实例
        /// </summary>
        /// <param name="level">日志输出级别</param>
        /// <param name="message">日志消息</param>
        /// <param name="exception">日志异常</param>
        /// <param name="isData">是否数据日志</param>
        protected override void Write(LogLevel level, object message, Exception exception, bool isData = false)
        {
            if (!isData)
            {
                return;
            }
            IEnumerable<DataLog> dataLogs = message as IEnumerable<DataLog>;
            if (dataLogs == null)
            {
                return;
            }
            IDataLogCache logCache = ServiceProvider.GetService<IDataLogCache>();
            foreach (DataLog dataLog in dataLogs)
            {
                logCache.AddDataLog(dataLog);
            }
        }
    }
}