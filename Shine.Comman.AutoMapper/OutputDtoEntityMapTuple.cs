using System;
using AutoMapper;
using Shine.Core.Mapping;
using Shine.Core.Security;


namespace Shine.Comman.AutoMapper
{
    /// <summary>
    /// 实体-输出DTO映射配对
    /// </summary>
    public class OutputDtoEntityMapTuple : MapTupleBase<EntityTypeFinder, OutputDtoTypeFinder>
    {
        /// <summary>
        /// 重写以定义源类型与目标类型的匹配规则
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        protected override bool IsMatch(Type sourceType, Type targetType)
        {
            const string end = "OutDto";
            return targetType.Name.Contains(sourceType.Name + end); 
        }

        /// <summary>
        /// 重写以实现映射类型的创建
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        protected override void CreateMapper(Type sourceType, Type targetType)
        {
            Mapper.CreateMap(sourceType,targetType);
        }
    }
}