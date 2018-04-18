
using Shine.Comman.Data;
using Shine.Comman.Extensions;

namespace Shine.Core.Security
{
    /// <summary>
    /// 权限检查结果
    /// </summary>
    public sealed class AuthorizationResult : ComResult<AuthorizationResultType>
    {
        static AuthorizationResult()
        {
            Allowed = new AuthorizationResult(AuthorizationResultType.Allowed);
        }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationResultType type)
            : this(type, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationResultType type, string message)
            : this(type, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationResultType type, string message, object data)
            : base(type, message, data)
        { }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public override string Message
        {
            get { return _message ?? ResultType.ToDescription(); }
            set { _message = value; }
        }

        /// <summary>
        /// 获取或设置 允许的检查结果
        /// </summary>
        public static AuthorizationResult Allowed { get; set; }
    }
}