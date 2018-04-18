using Shine.Comman.Secutiry;
using Shine.Core;
using Shine.Core.Caching;
using Shine.Core.Caching.Models;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.UserManager;
using Shine.Web.WebApi;
using Shine.Web.WebApi.Filters;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Shine.WebApi.Controllers
{
    /// <summary>
    /// 所有接口的基类
    /// </summary>
    [OperateLogFilter]
    public abstract class ThisBaseApiController : BaseApiController
    {
        #region 数据查询服务集合
        /// <summary>
        /// 用户服务对象
        /// </summary>
        public IUserLoginContract UserLoginService { set; get; }

        /// <summary>
        /// 主机服务对象
        /// </summary>
        public IHostContract HostService { set; get; }

        /// <summary>
        /// 组织机构服务对象
        /// </summary>
        public IOrganizeContract OrganizeService { set; get; }

        public IHostPolicyContract HostPolicyService { set; get; }

        public IDataItemContract DataItemService { set; get; }

        public IUserBlackListContract UserBlacklistService { set; get; }

        public ILightPoleContract LightPoleService { set; get; }

        public ISubControlContract SubControlService { set; get; }

        public ILightPlanContract LightPlanService { set; get; }

        public IElectricityContract ElectricityService { set; get; }

        public IGroupControlContract GroupControlService { set; get; }

        public ISum_PowerContract Sum_PowerService { set; get; }

        public IUpgradeContract UpgradeService { set; get; }

        #endregion

        /// <summary>
        /// 获取当前登录的缓存用户
        /// </summary>
        protected CacheUser GetCacheUser => ActionContext.Request.Headers.TryCatchAction(
            action: m =>
            {
                CacheUser userLogin;
                ICache iCache = CacheManager.GetCacher<CacheUser>();
                if (m.TryGetValues(Constants.CurrentUserKey, out IEnumerable<string> keyvalues)
                    &&
                    m.TryGetValues(Constants.CurrentUserUID, out IEnumerable<string> uidvalues))
                {
                    string key = keyvalues.First();
                    userLogin = iCache.Get<CacheUser>(key);

                    if (userLogin==null||Convert.ToString(userLogin.Id).AESEncrypt128(userLogin.SecretKey) != uidvalues.First())
                    {
                        throw new Exception("id:验证用户信息不合法！");
                    }
                    return userLogin;
                }
                else
                {
                    throw new Exception("id:未查找到用户登录信息");
                }
            });

        /// <summary>
        /// 获取当前用户管理的组织机构ID列表
        /// </summary>
        protected List<Guid> ListOrganizeId => UserLoginService.TryCatchAction(
             action: m =>
             {
                 return m.GetUserMaptoOrganize(GetCacheUser.Id)
                         .Select(b => b.OrganizeOne.Id)
                         .Distinct()
                         .ToList();
             });

        /// <summary>
        /// 获取当前用户的操作权限列表
        /// </summary>
        protected string[] IntPermissionList => UserLoginService.TryCatchAction(
            action: m =>
            {
               return m.IQUserLogins.Where(a => a.Id == GetCacheUser.Id)
                             .FirstOrDefault()
                             .PermissionList
                             .ToString()
                             .Split(',');
            });

        /// <summary>
        /// 获取用户管理组织机构下的主机信息
        /// </summary>
        protected IQueryable<Host> MyHostqueryable
        {
            get
            {
                return from a in HostService.HostQueryable
                       join b in ListOrganizeId
                       on a.Organize_Id equals b
                       select a;
            }
        }
    }
}
