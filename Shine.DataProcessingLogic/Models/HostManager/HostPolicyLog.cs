using Shine.DataProcessingLogic.Base.HostManager.Models;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shine.DataProcessingLogic.Models.HostManager
{
    /// <summary>
    /// 路灯主机的策略下载日志实体
    /// </summary>
    [Description("路灯主机的策略下载日志实体")]
    public class HostPolicyLog : HostPolicyLogBase<Guid>
    {

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象主键
        /// </summary>
        public Guid Organzie_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Organize"/>实体对象
        /// </summary>
        public virtual Organize OrganizeOne { set; get; }



        /// <summary>
        /// 获取或设置 关联的 <see cref="DataItemDetail"/> 类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 关联的 <see cref="DataItemDetail"/> 类型
        /// </summary>

        public DataItemDetail DataItemDetailOne { set; get; }



        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体
        /// </summary>
        public virtual Host HostOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>表数据实体主键
        /// </summary>
        public Guid Host_Id { set; get; }



        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 表数据实体主键
        /// </summary>
        public Guid UserLogin_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/> 表数据实体
        /// </summary>
        public virtual UserLogin UserLoginOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicy"/> 表数据实体
        /// </summary>

        public virtual HostPolicy HostPolicyOne { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicy"/> 表数据实体主键
        /// </summary>
        public Guid HostPolicy_Id { set; get; }
    }
}
