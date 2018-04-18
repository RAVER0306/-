using Shine.Comman.Data;
using System;
using System.ComponentModel;

namespace Shine.Core.Security
{
    /// <summary>
    /// 实体类——实体信息
    /// </summary>
    [Description("权限-实体信息")]
    public class EntityInfo : EntityInfoBase<Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityInfo"/>类型的新实例
        /// </summary>
        public EntityInfo()
        {
            Id = CombHelper.NewComb();
        }
    }
}