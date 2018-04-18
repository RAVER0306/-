namespace Shine.Comman.Data
{
    /// <summary>
    /// 通用结果基类
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public abstract class ComResult<TResultType> : ComResult<TResultType, object>, IComResult<TResultType>
    {
        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ComResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ComResult(TResultType type)
            : this(type, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ComResult(TResultType type, string message)
            : this(type, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType}"/>类型的新实例
        /// </summary>
        protected ComResult(TResultType type, string message, object data)
            : base(type, message, data)
        { }
    }


    /// <summary>
    /// 通用结果基类
    /// </summary>
    /// <typeparam name="TResultType">结果类型</typeparam>
    /// <typeparam name="TData">结果数据类型</typeparam>
    public abstract class ComResult<TResultType, TData> : IComResult<TResultType, TData>
    {
        /// <summary>
        /// 信息
        /// </summary>
        protected string _message;

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ComResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ComResult(TResultType type)
            : this(type, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ComResult(TResultType type, string message)
            : this(type, message, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="ComResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected ComResult(TResultType type, string message, TData data)
        {
            ResultType = type;
            _message = message;
            Data = data;
        }

        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        public TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public virtual string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        public TData Data { get; set; }
    }
}