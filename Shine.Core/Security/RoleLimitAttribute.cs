using System;


namespace Shine.Core.Security
{
    /// <summary>
    /// 指定功能只允许特定角色可以访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleLimitAttribute : Attribute
    { }
}