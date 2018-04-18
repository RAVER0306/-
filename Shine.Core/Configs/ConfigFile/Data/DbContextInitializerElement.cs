﻿using System.Configuration;

namespace Shine.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据上下文初始化配置节点
    /// </summary>
    internal class DbContextInitializerElement : ConfigurationElement
    {
        private const string TypeKey = "type";
        private const string EntityMapperFilesKey = "mapperFiles";
        private const string CreateDatabaseInitializerKey = "createInitializer";

        /// <summary>
        /// 获取或设置 初始化配置类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string InitializerTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }

        /// <summary>
        /// 获取或设置 实体映射类所在程序集名称字符串
        /// </summary>
        [ConfigurationProperty(EntityMapperFilesKey, IsRequired = true)]
        public virtual string EntityMapperFiles
        {
            get { return (string)this[EntityMapperFilesKey]; }
            set { this[EntityMapperFilesKey] = value; }
        }

        /// <summary>
        /// 获取或设置 数据库创建策略配置
        /// </summary>
        [ConfigurationProperty(CreateDatabaseInitializerKey)]
        public virtual CreateDatabaseInitializerElement CreateDatabaseInitializer
        {
            get { return (CreateDatabaseInitializerElement)this[CreateDatabaseInitializerKey]; }
            set { this[CreateDatabaseInitializerKey] = value; }
        }
    }
}