using AutoMapper;
using Shine.Core.Mapping;
using Shine.Core.Security;
using System;

namespace Shine.Comman.AutoMapper
{
    /// <summary>
    /// 输入DTO-实体映射配对
    /// </summary>
    public class InputDtoEntityMapTuple : MapTupleBase<InputDtoTypeFinder, EntityTypeFinder>
    {
        /// <summary>
        /// 重写以定义源类型与目标类型的匹配规则
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        protected override bool IsMatch(Type sourceType, Type targetType)
        {
            const string end = "InputDto";
            return sourceType.Name == targetType.Name + end;
        }

        /// <summary>
        /// 重写以实现映射类型的创建
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        protected override void CreateMapper(Type sourceType, Type targetType)
        {
            Mapper.CreateMap(sourceType, targetType);
        }
    }
}