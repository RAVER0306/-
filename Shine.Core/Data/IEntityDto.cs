namespace Shine.Core.Data
{
    /// <summary>
    /// 定义输入DTO
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IInputDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// 定义输入DTO
    /// </summary>
    public interface IInputDto { }

    /// <summary>
    /// 定义输出DTO
    /// </summary>
    public interface IOutputDto
    { }
}