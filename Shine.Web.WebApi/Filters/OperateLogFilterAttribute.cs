using Shine.Core.Context;
using Shine.Core.Extensions;
using Shine.Core.Logging;
using Shine.Core.Security;
using Shine.Web.WebApi.Extensions;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Filters;

namespace Shine.Web.WebApi.Filters
{
    /// <summary>
    /// 操作日志记录过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OperateLogFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取或设置 数据日志缓存
        /// </summary>
        public IDataLogCache DataLogCache { get; set; }

        /// <summary>
        /// 获取或设置 操作日志输出者
        /// </summary>
        public IOperateLogWriter OperateLogWriter { get; set; }

        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IFunction function = actionExecutedContext.Request.GetExecuteFunction(ServiceProvider);
            if (function == null || !function.OperateLogEnabled)
            {
                return;
            }
            Operator @operator = new Operator()
            {
                Ip = actionExecutedContext.Request.GetClientIpAddress()
            };
            IIdentity identity = actionExecutedContext.ActionContext.RequestContext.Principal.Identity;
            if (identity.IsAuthenticated)
            {
                ClaimsIdentity user = identity as ClaimsIdentity;
                if (user != null)
                {
                    @operator.UserId = user.GetClaimValueFirstOrDefault(ClaimTypes.NameIdentifier);
                    @operator.Name = user.GetClaimValueFirstOrDefault(ClaimTypes.Name);
                    @operator.NickName = user.GetClaimValueFirstOrDefault(ClaimTypes.GivenName);
                }
            }
            OperateLog operateLog = new OperateLog()
            {
                FunctionName = function.Name,
                Operator = @operator
            };
            if (function.DataLogEnabled)
            {
                foreach (DataLog dataLog in DataLogCache.DataLogs)
                {
                    operateLog.DataLogs.Add(dataLog);
                }
            }
            OperateLogWriter.Write(operateLog);
        }

    }
}