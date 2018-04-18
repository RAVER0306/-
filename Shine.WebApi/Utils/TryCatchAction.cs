using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Logging;
using System;
using System.Threading.Tasks;

namespace Shine.WebApi.Utils
{
    /// <summary>
    /// 接口请求异常抓取处理
    /// </summary>
    public static class TryCatchActions
    {
        private static readonly ILogger logger = LogManager.GetLogger("TryCatchActions");

        /// <summary>
        /// 接口异常抓取默认处理方法
        /// </summary>
        /// <typeparam name="T">源操作对象类型</typeparam>
        /// <typeparam name="TResult">返回的目标类型</typeparam>
        /// <param name="source">源操作对象</param>
        /// <param name="action">执行的主方法</param>
        /// <param name="failureAction">执行失败处理的方法</param>
        /// <returns>返回操作结果</returns>
        public static TResult TryCatchAction<T, TResult>(this T source, Func<T, TResult> action, Func<Exception, TResult> failureAction=null)
            where T : class
        {
            TResult result;
            try
            {
                result = action(source);
            }
            catch (Exception ex)
            {
                if (failureAction == null)
                {
                    throw ex;
                }
                else
                {
                    result = failureAction(ex);
                }
            }
            return result;
        }

        /// <summary>
        /// 接口异常抓取默认处理方法
        /// </summary>
        /// <typeparam name="T">源操作对象类型</typeparam>
        /// <param name="source">源操作对象</param>
        /// <param name="action">执行的主方法</param>
        /// <param name="failureAction">执行失败处理的方法</param>
        /// <returns>返回操作结果</returns>
        /// <returns></returns>
        public static OperationResult TryCatchAction<T>(this T source, Func<T, OperationResult> action, Func<Exception, OperationResult> failureAction = null)
        {
            OperationResult result;
            try
            {
                result = action(source);
            }
            catch (Exception ex)
            {
                if (failureAction == null)
                {
                    switch (ex)
                    {
                        case Exception m when (m is ArgumentException || m is ArgumentNullException):
                            result = new OperationResult(OperationResultType.ValidError, ex.Message);
                            break;
                        case Exception m when (ex.Message.Contains("id:")):
                            result = new OperationResult(OperationResultType.Error, ex.Message.Substring(3));
                            break;
                        default:
                            string Id = new Random().NextNumberString(12);
                            logger.Error($"{Id}:{ex}");
                            result = new OperationResult(OperationResultType.Error, $"出现未标识错误:{Id}，请联系管理员");
                            break;
                    }
                }
                else
                {
                    result = failureAction(ex);
                }
            }
            return result;
        }

        /// <summary>
        /// 接口异常抓取默认处理方法
        /// </summary>
        /// <typeparam name="T">源操作对象类型</typeparam>
        /// <typeparam name="TResult">返回对象类型</typeparam>
        /// <param name="source">源操作对象</param>
        /// <param name="action">执行的主方法</param>
        /// <param name="failureAction">执行失败处理的方法</param>
        /// <returns>返回操作结果</returns>
        public static async Task<TResult> TryCatchActionAsync<T, TResult>(this T source, Func<T, Task<TResult>> action, Func<Exception, TResult> failureAction)
            where T : class
        {
            TResult result;
            try
            {
                result = await action(source);
            }
            catch (Exception ex)
            {
                result = failureAction(ex);
            }
            return result;
        }

        /// <summary>
        /// 接口异常抓取默认处理方法
        /// </summary>
        /// <typeparam name="T">源操作对象类型</typeparam>
        /// <param name="source">源操作对象</param>
        /// <param name="action">执行的主方法</param>
        /// <param name="failureAction">执行失败处理的方法</param>
        /// <returns>返回操作结果</returns>
        public static async Task<OperationResult> TryCatchActionAsync<T>(this T source, Func<T, Task<OperationResult>> action, Func<Exception, OperationResult> failureAction = null)
            where T : class
        {
            OperationResult result;
            try
            {
                result = await action(source);
            }
            catch (Exception ex)
            {
                if (failureAction == null)
                {
                    switch (ex)
                    {
                        case Exception m when (m is ArgumentException || m is ArgumentNullException):
                            result = new OperationResult(OperationResultType.ValidError, ex.Message);
                            break;
                        case Exception m when (ex.Message.Contains("id:")):
                            result = new OperationResult(OperationResultType.Error, ex.Message.Substring(3));
                            break;
                        default:
                            string Id = new Random().NextNumberString(12);
                            logger.Error($"{Id}:{ex}");
                            result = new OperationResult(OperationResultType.Error, $"出现未标识错误:{Id}，请联系管理员");
                            break;
                    }
                }
                else
                {
                    result = failureAction(ex);
                }
            }
            return result;
        }
    }
}