using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Shine.WebApi.Utils
{
    /// <summary>
    /// 封装 <see cref="HttpResponseMessage"/> 错误信息返回
    /// </summary>
    public static class CatchException
    {
        private static readonly ILogger logger = LogManager.GetLogger("CatchException");
        /// <summary>
        /// 获取<see cref="HttpResponseMessage"/> 错误信息
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="code">错误码</param>
        /// <param name="isSubstring">是否要截取字符串</param>
        /// <param name="unknown">是否未标识错误</param>
        /// <returns></returns>
        public static HttpResponseMessage CatchExceptionMsg(this Exception ex, HttpStatusCode code, bool isSubstring = false, bool unknown = false)
        {
            string Id = string.Empty;
            if (unknown)
            {
                Id = new Random().NextNumberString(12);
                logger.Error($"{Id}:{ex}");
            }
            var resp = new HttpResponseMessage(code)
            {
                //封装处理异常信息，返回指定JSON对象
                Content = new StringContent(JsonHelper.ToJson(
                         new OperationResult
                         {
                             ResultType = OperationResultType.Error,
                             Message = unknown ? $"出现未标识错误:{Id}，请联系管理员" : (isSubstring ? ex.Message.Substring(3).ToString() : ex.Message),
                             Data = null
                         }), Encoding.UTF8, "text/json"),
                ReasonPhrase = "Exception"
            };
            return resp;
        }
    }
}