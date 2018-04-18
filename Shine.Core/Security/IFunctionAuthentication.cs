using Shine.Core.Dependency;
using System.Security.Principal;

namespace Shine.Core.Security
{
    /// <summary>
    /// 定义功能权限验证器
    /// </summary>
    public interface IFunctionAuthorization : ISingletonDependency
    {
        /// <summary>
        /// 验证当前用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要验证的功能</param>
        /// <returns>功能权限验证结果</returns>
        AuthorizationResult Authorize(IFunction function);

        /// <summary>
        /// 验证用户是否有执行指定功能的权限
        /// </summary>
        /// <param name="function">要验证的功能</param>
        /// <param name="principal">在线用户信息</param>
        /// <returns>功能权限验证结果</returns>
        AuthorizationResult Authorize(IFunction function, IPrincipal principal);
    }
}