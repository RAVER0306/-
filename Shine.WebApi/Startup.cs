using AutoMapper;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Shine.Comman.AutoMapper;
using Shine.Comman.Log4Net;
using Shine.Core.Caching;
using Shine.Core.Dependency;
using Shine.Data.EF;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.Out;
using Shine.DataProcessingLogic.Dtos.UserManager;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.DataProcessingLogic.Models.UserManager;
using Shine.Web.Mvc.Autofac.Mvc;
using Shine.Web.Mvc.Initialize;
using Shine.Web.WebApi.Autofac.Http;
using Shine.Web.WebApi.Initialize;
using Shine.WebApi;
using Shine.WebApi.Models;
using Shine.WebApi.Models.OutDtos;

[assembly: OwinStartup(typeof(Startup))]

namespace Shine.WebApi
{
    /// <summary>
    /// 启动初始化
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 启动初始化配置
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            ICacheProvider provider = new RuntimeMemoryCacheProvider();
            CacheManager.SetProvider(provider, CacheLevel.First);

            IServicesBuilder builder = new ServicesBuilder();
            IServiceCollection services = builder.Build();
            services.AddLog4NetServices();
            services.AddDataServices();
            services.AddAutoMapperServices();

            IIocBuilder mvcIocBuilder = new MvcAutofacIocBuilder(services);
            app.UseShineMvc(mvcIocBuilder);
            IIocBuilder apiIocBuilder = new WebApiAutofacIocBuilder(services);
            app.UseShineWebApi(apiIocBuilder);
            app.ConfigureWebApi();

            //手动创建映射
            Mapper.CreateMap<UserPageOutDto, UserPageIn>();
            Mapper.CreateMap<UserPageIn, UserLogin>();
            Mapper.CreateMap<UserPageView, UserPageOutDto>();
            Mapper.CreateMap<Organize, OrganizePageOutDto>();
            Mapper.CreateMap<OrganizeIn, Organize>();
            Mapper.CreateMap<Host, HostPageOutDto>()
                  .ForMember(m => m.DataItemDetailName, opt => { opt.MapFrom(m => m.DataItemDetailOne.FullName); })
                  .ForMember(m => m.OrganizeName, opt => { opt.MapFrom(m => m.OrganizeOne.FullName); })
                  .ForMember(m => m.OrganId, opt => { opt.MapFrom(m => m.OrganizeOne.Id); })
                  .ForMember(m => m.DataItemDetailIndex, opt => { opt.MapFrom(m => m.DataItemDetailOne.Index); });
            Mapper.CreateMap<HostLogin, HostLoginPageOutDto>()
                  .ForMember(m => m.HostRegPacket, opt => { opt.MapFrom(m => m.HostOne.RegPackage); })
                  .ForMember(m => m.HostName, opt => { opt.MapFrom(m => m.HostOne.FullName); });
            Mapper.CreateMap<HostParameter,HostParameterPageOutDto>()
                  .ForMember(m => m.HostRegPacket, opt => { opt.MapFrom(m => m.HostOne.RegPackage); })
                  .ForMember(m => m.HostName, opt => { opt.MapFrom(m => m.HostOne.FullName); });
            Mapper.CreateMap<HostPolicy,HostPolicyPageOutDto>()
                  .ForMember(m => m.HostRegPacket, opt => { opt.MapFrom(m => m.HostOne.RegPackage); })
                  .ForMember(m => m.HostName, opt => { opt.MapFrom(m => m.HostOne.FullName); });
            Mapper.CreateMap<HostPolicyLogView, HostPolicyLogOutDto>();
            Mapper.CreateMap<DataItem, DataItemPageOutDto>();
            Mapper.CreateMap<BlackListView, BlackListPageOutDto>();
            Mapper.CreateMap<LightPoleView, LightPolePageOutDto>();
            Mapper.CreateMap<SubControlView, SubControlPageOutDto>();
            Mapper.CreateMap<LightPlanView, LightPlanPageOutDto>();
            Mapper.CreateMap<GroupView, GroupPageOutDto>();
            Mapper.CreateMap<HostReadTimeDataView, HTDPageOutDto>();
            Mapper.CreateMap<SubReadTimeDataView, SRTDPageOutDto>();
            Mapper.CreateMap<SubControl, SubControlView>();
            Mapper.CreateMap<Information, InformationOutDto>()
                  .ForMember(m => m.TypeName, opt => { opt.MapFrom(m => m.DataItemDetailOne.FullName); })
                  .ForMember(m => m.TypeIndex, opt => { opt.MapFrom(m => m.DataItemDetailOne.Index); });
            Mapper.CreateMap<InformationView, InformationPageOut>();
            Mapper.CreateMap<SubRealTimeData_0x16_In, SubRealTimeData>();
            Mapper.CreateMap<GroupControl_0x18_In, GroupControl>()
                  .ForSourceMember(m => m.RegPackage, opt => { opt.Ignore(); });
            Mapper.CreateMap<LightPlan_0x54_In, LightPlan>()
                  .ForSourceMember(m => m.RegPackage, opt => { opt.Ignore(); });
            Mapper.CreateMap<LightPlan_0x59_In,LightPlan>()
                  .ForSourceMember(m => m.RegPackage, opt => { opt.Ignore(); });
            Mapper.CreateMap<HostParameter_0x25_In,HostParameter>()
                  .ForSourceMember(m => m.RegPackage, opt => { opt.Ignore(); }); 
            Mapper.CreateMap<HostLogin_0x44_In,HostLogin>()
                  .ForSourceMember(m => m.RegPackage, opt => { opt.Ignore(); });
            Mapper.CreateMap<HostPolicy_0x5B_In, HostPolicy>()
                 .ForSourceMember(m => m.RegPackage, opt => { opt.Ignore(); });
            Mapper.CreateMap<UpgradeLog, UpgradeLogOutDto>()
                 .ForMember(m => m.OrganizeName, opt => { opt.MapFrom(m => m.OrganizeOne.FullName); });
        }
    }
}
