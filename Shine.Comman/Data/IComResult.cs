namespace Shine.Comman.Data
{
    /// <summary>
    /// Com操作结果
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public interface IComResult<TResultType> : IComResult<TResultType, object>
    { }


    /// <summary>
    /// Com操作结果
    /// </summary>
    public interface IComResult<TResultType, TData>
    {
        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        TData Data { get; set; }
    }
}