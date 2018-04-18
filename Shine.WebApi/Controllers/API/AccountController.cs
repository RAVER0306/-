using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core;
using Shine.Core.Caching;
using Shine.Core.Data.Extensions;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.UserManager;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 用户帐号相关信息管理接口
    /// </summary>
    [Description("用户帐号相关信息管理接口")]
    public class AccountController : ThisBaseApiController
    {
        #region 用户登录验证接口
        /// <summary>
        /// 用户登录验证接口
        /// </summary>
        /// <param name="uname">用户帐号</param>
        /// <param name="pwd">用户密码</param>
        /// <returns>登录结果关联信息</returns>
        [AllowAnonymous]
        [HttpPost]
        [Description("用户请求登录验证")]
        [Route("Account/Login")]
        public async Task<IHttpActionResult> Login(string uname, string pwd) => Json(await UserLoginService.TryCatchActionAsync(
                 action: m =>
                 {
                     var context = HttpContext.Current;
                     uname.CheckNotNullOrEmpty("uname");
                     pwd.CheckNotNullOrEmpty("pwd");
                     return Task.Run(function: () =>
                     {
                         var result = UserLoginService.CheckToUserLogin(uname, pwd);
                         var outDto = result.Data as UserLoginOutDto;
                         var Uid = Convert.ToString(outDto.Id).AESEncrypt128(result.Message);
                         var UKey = Convert.ToString(outDto.UserName).AESEncrypt128();
                         context.Response.AddHeader(Constants.CurrentUserUID, Uid);
                         context.Response.AddHeader(Constants.CurrentUserKey, UKey);
                         result.Message = "用户登录验证成功!";
                         return result;
                     });
                 }));
        #endregion

        #region 用户头像上传更新
        /// <summary>
        /// 用户头像上传更新
        /// </summary>
        /// <param name="id">用户主键ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/ImgUpload/{id}")]
        [Description("用户头像上传更新")]
        public async Task<IHttpActionResult> ImgUpload(Guid id) => Json(await UserLoginService.TryCatchActionAsync(action: m =>
        {
            id.CheckNotEmpty("id");
            if (id != GetCacheUser.Id)
            {
                throw new Exception($"id:修改用户头像Id识别出错");
            }
            return UserLoginService.SetUserHeadIcon(id, Request.SaveUploadFile(id.ToString().ToUpper()));
        }));
        #endregion

        #region 用户头像图片获取
        /// <summary>  
        /// 用户头像图片获取
        /// </summary>  
        [HttpGet]
        [Route("Account/{id}/HeadImage")]
        [Description("用户头像获取")]
        public HttpResponseMessage HeadImage(string id) => UserLoginService.TryCatchAction<IUserLoginContract, HttpResponseMessage>(
                  action: m =>
                   {
                       id.CheckNotNullOrEmpty("id");
                       Guid gid = new Guid(id.AESDecrypt128());
                       var imgByte = UserLoginService.GetHeadIconBits(gid);
                       var resp = new HttpResponseMessage(HttpStatusCode.OK)
                       {
                           Content = new ByteArrayContent(imgByte)
                       };
                       resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                       return resp;
                   },
                  failureAction: b =>
                   {
                       switch (b)
                       {
                           case Exception ex when (ex is ArgumentException || ex is ArgumentNullException):
                               return ex.CatchExceptionMsg(HttpStatusCode.ExpectationFailed);
                           case Exception ex when (ex.Message.Contains("id:")):
                               return ex.CatchExceptionMsg(HttpStatusCode.NoContent, isSubstring: true);
                           default:
                               return b.CatchExceptionMsg(HttpStatusCode.InternalServerError, unknown: true);

                       }
                   });
        #endregion

        #region 添加新的用户信息
        /// <summary>
        /// 添加用户账号登陆信息
        /// </summary>
        /// <param name="UserSet">用户登陆基本信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/UserAdds")]
        [Description("添加用户")]
        public async Task<IHttpActionResult> UserAdds([FromBody]params UserLoginInputDto[] UserSet) => Json(await UserLoginService.TryCatchActionAsync(
                action: async m =>
                {
                    UserSet.CheckNotNullOrEmpty("UserSet");
                    var cacheUser = GetCacheUser;
                    if (!cacheUser.IsAdministrator)
                    {
                        foreach (var i in UserSet)
                        {
                            if (!ListOrganizeId.Contains(i.Organize_Id) && i.Organize_Id != cacheUser.Organize_Id)
                            {
                                throw new Exception($"id:添加用户的组织机构归属不在权限范围内");
                            }
                            foreach (var a in i.PermissionList.Split(','))
                            {
                                if (!IntPermissionList.Contains(a))
                                {
                                    throw new Exception($"id:添加的用户的操作权限归属不在权限范围内");
                                }
                            }
                        }
                    }
                    // 不管信息是否添加成功？都执行删除当前对象页的缓存
                    ICache cache = CacheManager.GetCacher<UserPageView>();
                    cache.Clear();
                    // --------------------------------------------
                    return await UserLoginService.AddUserLogin(GetCacheUser, UserSet);
                }));
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">请求格式:
        /// <para>
        /// [
        /// "C54CBFE0-4293-4A5F-9577-A852009DB95F",
        /// "36C312AA-0792-487F-B72B-A852009F5D23",
        /// ]       
        /// </para>
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/Delete")]
        [Description("删除用户")]
        public async Task<IHttpActionResult> Delete([FromBody] params Guid[] ids) => Json(await UserLoginService.TryCatchActionAsync(
                action: async m =>
                {
                    ids.CheckNotNullOrEmpty("ids");
                    //不管用户是否更新成功，都执行删除当前对象页的缓存
                    ICache cache = CacheManager.GetCacher<UserPageView>();
                    cache.Clear();
                    //----------------------------------------------
                    return await UserLoginService.DeleteUserLogin(GetCacheUser, ids);
                }));

        #endregion

        #region 更新用户基本信息
        /// <summary>
        /// 更新用户的基本信息
        /// </summary>
        /// <param name="uid">用户基本信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/UpadtedUser")]
        [Description("更新用户的基本信息")]
        public IHttpActionResult UpdatedUser([FromBody]UserInputDto uid) => Json(UserLoginService.TryCatchAction(
                action: m =>
                {
                    uid.CheckNotNull("uid");
                    if (uid.Id != GetCacheUser.Id)
                    {
                        throw new Exception("id:更新用户的基本信息Id识别错误");
                    }
                    return UserLoginService.UpdatedUser(uid);
                }));
        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="cpwd">修改用户密码ChangePassword实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Account/ChangePwd")]
        [Description("修改用户登录密码")]
        public IHttpActionResult ChangePassword([FromBody]ChangePasswordView cpwd) => Json(UserLoginService.TryCatchAction(
               action: a =>
               {
                   cpwd.CheckNotNull("cpwd");
                   if (GetCacheUser.Id != cpwd.Id)
                   {
                       throw new Exception("id:修改用户密码Id识别错误!");
                   }
                   if (cpwd.NewPassword == cpwd.OldPassword)
                   {
                       throw new Exception("id:新密码和旧密码相同");
                   }
                   return UserLoginService.ChangePasswrod(cpwd.Id, cpwd.OldPassword, cpwd.NewPassword);
               }));
        #endregion

        #region 获取用户信息列表
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="grid">
        ///  FilterGroup:{"Rules": [],"Groups": [],"Operate": 1}
        ///  SortField:Id,CreatedTime
        ///  SortOrder:desc,asc
        ///  排序字段对应一种排序方式
        /// </param>
        /// <returns></returns>
        [Description("获取用户信息列表")]
        [HttpPost]
        [Route("Account/GridData")]
        public IHttpActionResult GridData([FromBody]GridRequestsModel grid) => Json(UserLoginService.TryCatchAction(
                action: m =>
                {
                    grid.CheckNotNull("grid");
                    var cacheUser = GetCacheUser;
                    if (!cacheUser.Level.IsBetween(1, 2))
                    {
                        throw new Exception($"id:你的操作权限等级过低");
                    }

                    //查询条件
                    GridRequests request = new GridRequests(grid);
                    //添加默认排序，只有排序未设置的情况下生效
                    request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                    //获取查询集合
                    IQueryable<UserPageView> queryable = (from n in m.IQUserLogins
                                                      join b in m.IQUsers
                                                      on n.Id equals b.UserLogin_Id
                                                      where n.IsAdministrator == false
                                                      select (new UserPageView
                                                      {
                                                          Id = n.Id,
                                                          UserName = n.UserName,
                                                          IsLocked = n.IsLocked,
                                                          LockoutEnabled = n.LockoutEnabled,
                                                          OrganizeName = n.OrganizeOne.FullName,
                                                          NickName = b.NickName,
                                                          RealName = b.RealName,
                                                          Email = b.Email,
                                                          PhoneNumber = b.PhoneNumber,
                                                          CreatedTime = b.CreatedTime,
                                                          Organize_Id = n.Organize_Id,
                                                          LoginCount = n.LoginCount,
                                                          IsAdministrator = n.IsAdministrator,
                                                          Level = n.Level,
                                                          PermissionList = n.PermissionList,
                                                      })).AsQueryable();
                    Expression<Func<UserPageView, bool>> predicate = FilterHelper.GetExpression<UserPageView>(request.FilterGroup);
                    if (!cacheUser.IsAdministrator)
                    {
                        predicate = predicate.And(b => ListOrganizeId.Contains(b.Organize_Id));
                    }

                    var data = queryable.ToPageCache<UserPageView, UserPageOutDto>(predicate, request.PageCondition);

                    //把用户获取的用户信息主键缓存起来，留待修改时校正
                    var cacheUserId = data.ListData.Select(b => b.Id);
                    ICache iCache = CacheManager.GetCacher("CacheUserPageId");
                    iCache.Set(cacheUser.UserName.AESEncrypt128(), cacheUserId, TimeSpan.FromMinutes(30));

                    return new OperationResult(OperationResultType.Success, "获取数据成功", data);
                }));
        #endregion

        #region 基于获取的用户列表信息修改
        /// <summary>
        /// 基于获取的用户列表信息修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Description("基于获取的用户列表信息修改")]
        [HttpPost]
        [Route("Account/EditData")]
        public async Task<IHttpActionResult> EditData([FromBody]params UserPageOutDto[] user) => Json(await UserLoginService.TryCatchActionAsync(
                action: async m =>
                {
                    Logger.Debug(user.ToJsonString());
                    user.CheckNotNullOrEmpty("user");
                    UserPageIn[] InputUser = new UserPageIn[user.Length];
                    await Task.Run(() =>
                    {
                        var cacheUser = GetCacheUser;
                        ICache iCache = CacheManager.GetCacher("CacheUserPageId");
                        var cacheIds = iCache.Get(cacheUser.UserName.AESEncrypt128()) as IEnumerable<Guid>;
                        for (int n = 0; n < user.Length; n++)
                        {
                            var i = user[n];
                            if (!cacheUser.IsAdministrator)
                            {
                                if (cacheIds == null)
                                {
                                    throw new Exception("id:请先获取数据再进行操作！");
                                }
                                if (!cacheIds.Contains(i.Id))
                                {
                                    throw new Exception($"id:值主键{i.Id}不在范围内,请尝试重新获取数据后在操作...");
                                }
                                if (!ListOrganizeId.Contains(i.Organize_Id))
                                {
                                    throw new Exception($"id:用户主键{i.Id}的组织机构归属不在权限范围内");
                                }
                                if (i.Level <= cacheUser.Level)
                                {
                                    throw new Exception($"id:用户主键{i.Id}的权限等级归属不在权限范围内");
                                }
                                foreach (var a in i.PermissionList.Split(','))
                                {
                                    if (!IntPermissionList.Contains(a))
                                    {
                                        throw new Exception($"id:用户主键{i.Id}的操作权限归属不在权限范围内");
                                    }
                                }
                            }

                            UserPageIn userPageIn = i.MapTo<UserPageIn>();
                            userPageIn.LastUpdatorUserId = cacheUser.UserName;
                            InputUser[n] = userPageIn;
                        }
                    });
                    //不管用户是否更新成功，都执行删除当前对象页的缓存
                    ICache cache = CacheManager.GetCacher<UserPageView>();
                    cache.Clear();
                    //----------------------------------------------
                    return await m.EditUserData(InputUser);
                }));
        #endregion

        #region 用户信息标记已读
        /// <summary>
        /// 用户信息标记已读
        /// </summary>
        /// <param name="Ids">要标记为已读的信息主键</param>
        /// <returns></returns>
        [Description("用户信息标记已读")]
        [HttpPost]
        [Route("Account/MarkRead")]
        public IHttpActionResult MarkRead([FromBody] params Guid[] Ids) => Json(UserLoginService.TryCatchAction(
            action: m => 
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cache = GetCacheUser;
                return m.MarkRead(Ids);
            }));
        #endregion

        #region 删除指定的用户提示信息
        /// <summary>
        /// 删除指定的用户提示信息
        /// </summary>
        /// <param name="Ids">要删除的信息主键</param>
        /// <returns></returns>
        [Description("删除指定的用户提示信息")]
        [HttpPost]
        [Route("Account/DeleteInformation")]
        public IHttpActionResult DeleteInformation([FromBody]params Guid[] Ids) => Json(UserLoginService.TryCatchAction(
            action:m=> 
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cache = GetCacheUser;
                return m.DeleteInformation(Ids);
            }));
        #endregion

        #region 获取用户提示信息
        /// <summary>
        /// 删除指定的用户提示信息
        /// </summary>
        /// <param name="Ids">要删除的信息主键</param>
        /// <returns></returns>
        [Description("获取用户提示信息")]
        [HttpPost]
        [Route("Account/GetInformation")]
        public IHttpActionResult GetInformation([FromBody]GridRequestsModel grid) => Json(UserLoginService.TryCatchAction(
            action: m =>
             {
                 grid.CheckNotNull("grid");
                 var cacheUser = GetCacheUser;//获取缓存判断

                 //查询条件
                 GridRequests request = new GridRequests(grid);
                 //添加默认排序，只有排序未设置的情况下生效
                 request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                 var queryable = from c in m.QueryableInfo
                                 where c.UserLogin_Id == cacheUser.Id
                                 select new InformationView {
                                     CreatedTime = c.CreatedTime,
                                     Id = c.Id,
                                     IsReaded = c.IsReaded,
                                     ObjectId = c.ObjectId,
                                     TypeIndex = c.DataItemDetailOne.Index,
                                     TypeName = c.DataItemDetailOne.FullName
                                 };

                 Expression<Func<InformationView, bool>> predicate = FilterHelper.GetExpression<InformationView>(request.FilterGroup);
                 var data = queryable.ToPage<InformationView,InformationPageOut>(predicate, request.PageCondition);
                 return new OperationResult(OperationResultType.Success, "操作成功", data);
             }));
        #endregion
    }
}
