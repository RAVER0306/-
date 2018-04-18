using Shine.Core.Dependency;
using Shine.Core.Mapping;
using Shine.Core.Security;
using Shine.Comman;
using AutoMapper;

namespace Shine.Comman.AutoMapper
{
    /// <summary>
    /// 服务映射信息集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加AutoMapper服务映射信息
        /// </summary>
        public static void AddAutoMapperServices(this IServiceCollection services)
        {
            services.CheckNotNull("services" );
            services.AddSingleton<Core.Mapping.IMapper, AutoMapperMapper>();
            services.AddSingleton<InputDtoTypeFinder>();
            services.AddSingleton<OutputDtoTypeFinder>();
            services.AddSingleton<EntityTypeFinder>();
            services.AddSingleton<IMapTuple, InputDtoEntityMapTuple>();
            services.AddSingleton<IMapTuple, OutputDtoEntityMapTuple>();
        }
    }
}