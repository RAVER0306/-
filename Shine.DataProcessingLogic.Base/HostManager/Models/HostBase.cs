using Shine.Core.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shine.DataProcessingLogic.Base.HostManager.Models
{
    /// <summary>
    /// 路灯主机的基本信息实体基类
    /// </summary>
    /// <typeparam name="TKey">路灯主机的基本信息实体主键类型</typeparam>
    public abstract class HostBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime,
        ILockable
        where TKey : IEquatable<TKey>
    {
        /// <summary> 
        /// 获取或设置 主机服务手机号码 
        /// </summary> 
        [StringLength(32),Phone]
        public string PhoneNum { set; get; }

        /// <summary> 
        /// 获取或设置 手机号码的服务密码 
        /// </summary> 
        [StringLength(32)]
        public string PhoneServerPass { set; get; }

        /// <summary> 
        /// 获取或设置 路灯主机的名称 
        /// </summary> 
        [StringLength(64)]
        public string FullName { set; get; }

        /// <summary> 
        /// 获取或设置 路灯主机注册包 
        /// </summary> 
        [StringLength(16)]
        public string RegPackage { set; get; }

        /// <summary> 
        /// 获取或设置 路灯主机心跳包 
        /// </summary> 
        [StringLength(32)]
        public string HeartBag { set; get; }

        /// <summary> 
        /// 获取或设置 主机当前位置 
        /// </summary> 
        [StringLength(128)]
        public string Address { set; get; }

        /// <summary> 
        /// 获取或设置 主机信息备注说明 
        /// </summary> 
        [StringLength(512)]
        public string Remark { get; set; }


        /// <summary> 
        /// 获取或设置 手机号码运营商：
        /// PhoneOperator=0 未设置,
        /// PhoneOperator=1 中国联通,
        /// PhoneOperator=2 中国电信,
        /// PhoneOperator=3 中国移动,
        /// PhoneOperator=4 其他
        /// </summary> 
        [DefaultValue(0)]
        public byte PhoneOperator { set; get; }

        /// <summary> 
        /// 获取或设置 手机剩余流量 
        /// </summary> 
        [DefaultValue(0)]
        public int PhoneResidualFlow { set; get; }

        /// <summary> 
        /// 获取或设置 是否统计主机的能耗 
        /// </summary> 
        [DefaultValue(true)]
        public bool IsEnergySwitch { set; get; }

        /// <summary>
        /// 获取或设置 该数据创建的时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 该信息是否被锁定(冻结)
        /// </summary>
        public bool IsLocked { set; get; }
    }
}
