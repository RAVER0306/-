using System.ComponentModel;

namespace Shine.Core.Security
{
    /// <summary>
    /// 表示权限验证结果的枚举
    /// </summary>
    public enum AuthorizationResultType
    {
        /// <summary>
        /// 权限检查通过
        /// </summary>
        [Description("权限检查通过。")] Allowed,

        /// <summary>
        /// 该操作需要登录后才能继续进行
        /// </summary>
        [Description("该操作需要登录后才能继续进行。")] LoggedOut,

        /// <summary>
        /// 权限不足
        /// </summary>
        [Description("当前用户权限不足，不能继续操作。")] PurviewLack,

        /// <summary>
        /// 功能锁定
        /// </summary>
        [Description("当前功能已经被锁定，不能继续操作。")] FunctionLocked,

        /// <summary>
        /// 要执行的功能不存在
        /// </summary>
        [Description("指定功能不存在。")] FunctionNotFound,

        /// <summary>
        /// 出现错误
        /// </summary>
        [Description("权限检查出现错误")]
        Error
    }
}