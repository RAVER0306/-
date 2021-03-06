﻿using System.Configuration;

namespace Shine.Core.Configs.ConfigFile
{
    /// <summary>
    /// 日志配置节点
    /// </summary>
    internal class LoggingElement : ConfigurationElement
    {
        private const string LoggingEntryKey = "entry";
        private const string DataLoggingKey = "data";
        private const string BasicLoggingKey = "basic";

        /// <summary>
        /// 获取或设置 日志输入配置节点
        /// </summary>
        [ConfigurationProperty(LoggingEntryKey)]
        public virtual LoggingEntryElement LoggingEntry
        {
            get { return (LoggingEntryElement)this[LoggingEntryKey]; }
            set { this[LoggingEntryKey] = value; }
        }

        ///// <summary>
        ///// 获取或设置 数据日志配置节点
        ///// </summary>
        //[ConfigurationProperty(DataLoggingKey)]
        //public virtual DataLoggingElement DataLogging
        //{
        //    get { return (DataLoggingElement)this[DataLoggingKey]; }
        //    set { this[DataLoggingKey] = value; }
        //}

        /// <summary>
        /// 获取或设置 基础日志配置节点
        /// </summary>
        [ConfigurationProperty(BasicLoggingKey)]
        public virtual BasicLoggingElement BasicLogging
        {
            get { return (BasicLoggingElement)this[BasicLoggingKey]; }
            set { this[BasicLoggingKey] = value; }
        }

    }
}