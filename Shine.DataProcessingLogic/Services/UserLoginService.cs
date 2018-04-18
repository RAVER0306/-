using Shine.Comman;
using Shine.Comman.AppSetting;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Secutiry;
using Shine.Comman.Web;
using Shine.Core;
using Shine.Core.Caching;
using Shine.Core.Caching.Models;
using Shine.Core.Data;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.UserManager;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace Shine.DataProcessingLogic.Services
{
    public partial class UserLoginService : IUserLoginContract
    {

        #region 信息仓储对象
        /// <summary>
        /// 获取或设置 <see cref="UserLogin"/>的信息仓储对象
        /// </summary>
        public IRepository<UserLogin, Guid> UserLoginRepository { get; set; }

        /// <summary>
        /// 获取或设置 <see cref="Host"/>的信息仓储对象
        /// </summary>
        public IRepository<Host, Guid> HostRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/>的信息仓储对象
        /// </summary>
        public IRepository<SubControl, Guid> SubRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostPolicyLog"/>的信息仓储对象
        /// </summary>
        public IRepository<HostPolicyLog, Guid> HostPolicyLogRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="User"/>的信息仓储对象
        /// </summary>
        public IRepository<User, Guid> UserRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="UserOrganizeMap"/>的信息仓储对象
        /// </summary>
        public IRepository<UserOrganizeMap, Guid> UserOrganizeMapRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="Information"/>的信息仓储对象
        /// </summary>

        public IRepository<Information, Guid> InformationRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="DataItemDetail"/> 的信息仓储对象
        /// </summary>
        public IRepository<DataItemDetail, Guid> DataItemDetailRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="HostRealTimeData"/> 的信息仓储对象
        /// </summary>
        public IRepository<HostRealTimeData, Guid> HostRealTimeDataRepository { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="SubControl"/> 的信息仓储对象
        /// </summary>
        public IRepository<SubControl, Guid> SubControlRepository { set; get; }

        public IQueryable<Information> QueryableInfo => InformationRepository.Entities;
        #endregion

        #region 实现 IUserLoginContract 接口

        #region 用户登录验证
        /// <summary>
        /// 用户进行登录检查
        /// </summary>
        /// <param name="UserName">用户帐号</param>
        /// <param name="Passwork">用户密码</param>
        /// <returns>用户登录成功的关联集合</returns>
        public OperationResult CheckToUserLogin(string userName, string passwork)
        {
            if (!UserLoginRepository.CheckExists(m => m.UserName == userName))
            {
                throw new Exception($"id:{userName}帐号不存在");
            }
            else
            {
                var user = UserLoginRepository.TrackEntities.FirstOrDefault(m => m.UserName == userName);
                var diffTime = DateTime.Now.Subtract(user.LockoutEndDateUtc).TotalMinutes;

                // 判断帐号是否被登录锁定
                if (user.LockoutEnabled && user.AccessFailedCount >= 15 && diffTime < 0)
                {
                    throw new Exception($"id:帐号当前登录已被锁定，请在{user.LockoutEndDateUtc}后重试");
                }

                string _passwork = passwork.AESEncrypt128(user.SecretKey);
                if (user.Password != _passwork)
                {
                    if (user.LockoutEnabled)
                    {
                        //密码锁时间已经过去，重置密码锁相关信息
                        if (diffTime > 10)
                        {
                            user.LockoutEndDateUtc = DateTime.Now;
                            user.AccessFailedCount = 1;
                            UserLoginRepository.Update(user);
                            throw new Exception($"id:登录密码错误，离帐号被登录锁定还剩{15 - user.AccessFailedCount}次机会");
                        }
                        else
                        {
                            user.LockoutEndDateUtc = DateTime.Now;
                            if (++user.AccessFailedCount >= 15)
                            {
                                user.LockoutEndDateUtc = DateTime.Now.AddMinutes(10);
                            }
                            UserLoginRepository.Update(user);
                            throw new Exception($"id:登录密码错误，离帐号被登录锁定还剩{15 - user.AccessFailedCount}次机会");
                        }
                    }
                    else
                    {
                        throw new Exception($"id:{userName}输入的帐号密码有误！");
                    }
                }
                else
                {
                    //用户是否被冻结
                    if (user.IsLocked)
                    {
                        throw new Exception($"id:{userName}帐号已经被冻结，请联系管理员解封！");
                    }
                    user.AccessFailedCount = 0;
                    user.LockoutEndDateUtc = DateTime.Now;
                    user.LockoutEndDateUtc = DateTime.Now;
                    user.LoginCount++;
                    user.FirstVisitTime = user.FirstVisitTime.Year == 1 ? DateTime.Now : user.FirstVisitTime;
                    user.PreviousVisitTime = user.LastVisitTime;
                    user.LastVisitTime = DateTime.Now;
                    UserLoginRepository.Update(user);
                    UserLoginOutDto ulod = user.MapTo<UserLoginOutDto>();

                    var topInfo20 = ulod.InformationMany.OrderBy(m => m.CreatedTime).Take(10).ToList();
                    ulod.InformationMany.Clear();

                    //获取用户的通知信息的一些关联属性
                    topInfo20.ForEach(
                        o =>
                        {
                            switch (o)
                            {
                                case InformationOutDto i when i.TypeIndex >= 0 && i.TypeIndex <= 10:
                                    var objResult1 = HostRepository.Entities.Where(m => m.Id == o.ObjectId).ToList();
                                    if (objResult1.Count > 0)
                                    {
                                        o.ObjectResult = objResult1.Select(m => new
                                        {
                                            OrganizeName = m.OrganizeOne.FullName,
                                            RegPackage = m.RegPackage,
                                            HostFullName = m.FullName
                                        }).First();
                                    }
                                    break;
                                case InformationOutDto i when i.TypeIndex >= 11 && i.TypeIndex <= 20:
                                    var objResult2 = SubRepository.Entities.Where(m => m.Id == o.ObjectId).ToList();
                                    if (objResult2.Count > 0)
                                    {
                                        o.ObjectResult = objResult2.Select(m => new
                                        {
                                            OrganizeName = m.SubAggregationMany.Count > 0 ? m.SubAggregationMany.First().OrganizeOne.FullName : null,
                                            RegPackage = m.SubAggregationMany.Count > 0 ? m.SubAggregationMany.First().HostOne.RegPackage : null,
                                            HostFullName = m.SubAggregationMany.Count > 0 ? m.SubAggregationMany.First().HostOne.FullName : null,
                                            SubNum = m.SubNum,
                                            FullName = m.SubName,
                                        }).First();
                                    }
                                    break;
                            }
                            ulod.InformationMany.Add(o);
                        });
                    ulod.UserMany.First().HeadIconPath = ulod.Id.ToString().AESEncrypt128();
                    ulod.OrganizeOne.OrganizeLogoPath = ulod.OrganizeOne.Id.ToString().AESEncrypt128();

                    //设置缓存
                    ICache iCache = CacheManager.GetCacher<CacheUser>();
                    int.TryParse($"LoginTimeOut".GetValue(), out int cacheTimeOut);
                    cacheTimeOut = cacheTimeOut == 0 ? 30 : cacheTimeOut;
                    iCache.Set(
                        key: user.UserName.AESEncrypt128(),
                        value: new CacheUser(user.Id, user.UserName, user.SecretKey, user.IsAdministrator, user.Level, user.Organize_Id),
                        slidingExpiration: TimeSpan.FromMinutes(cacheTimeOut));

                    //返回结果
                    return new OperationResult
                    {
                        ResultType = OperationResultType.Success,
                        Message = $"{user.SecretKey}",
                        Data = ulod
                    };
                }
            }
        }
        #endregion

        #region 更新用户头像
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="id">用户主键ID</param>
        /// <param name="imageFile">图片文件</param>
        /// <returns>是否修改成功</returns>
        public async Task<OperationResult> SetUserHeadIcon(Guid id, Task<FileInfo> imageFile)
        {
            if (!UserLoginRepository.CheckExists(m => m.Id == id))
            {
                throw new Exception($"id:用户主键{id}的账号不存在");
            }
            else
            {
                var file = await imageFile;
                using (Bitmap bitmap = new Bitmap(file.FullName))
                {
                    byte[] HeadIcomBits = bitmap.ToBytes();
                    var user = UserLoginRepository.TrackEntities.FirstOrDefault(m => m.Id == id);
                    user.UserMany.First().HeadIcon = HeadIcomBits;
                    user.UserMany.First().HeadIconPath = Path.Combine("~/HeadIconFiles", file.Name);
                    if (UserLoginRepository.Update(user) > 0)
                    {
                        return new OperationResult(OperationResultType.Success, "操作成功！");
                    }
                    else
                    {
                        return new OperationResult(OperationResultType.Error, "操作失败!");
                    }
                }
            }
        }
        #endregion

        #region 获取用户头像byte[]
        /// <summary>
        /// 获取用户头像byte[]
        /// </summary>
        /// <param name="id">用户主键</param>
        /// <returns></returns>
        public byte[] GetHeadIconBits(Guid id)
        {
            if (!UserLoginRepository.CheckExists(m => m.Id == id))
            {
                throw new Exception($"id:查询信息不能存在");
            }
            else
            {
                var result = UserLoginRepository.TrackEntities.First(m => m.Id == id);
                var user = result.UserMany.First();
                if (string.IsNullOrEmpty(user.HeadIconPath) && (user.HeadIcon?.Length <= 0 || user.HeadIcon == null))
                {
                    return Properties.Resources._default.ToBytes();
                }
                else
                {
                    var imgPath = HttpContext.Current.Server.MapPath($"{user.HeadIconPath}");
                    if (File.Exists(imgPath))
                    {
                        return File.ReadAllBytes(imgPath);
                    }
                    else
                    {
                        imgPath = HttpContext.Current.Server.MapPath($"~/HeadIconFiles\\{result.Id.ToString()}");
                        string str = user.HeadIcon.CreateImageFromBytes(imgPath);
                        user.HeadIconPath = $"~/HeadIconFiles\\{str.Substring(str.LastIndexOf('\\') + 1)}";
                        UserLoginRepository.Update(result);
                        return user.HeadIcon;
                    }
                }
            }
        }

        #endregion

        #region 添加用户信息
        /// <summary>
        /// 添加用户登录信息
        /// </summary>
        /// <param name="inputDtos">要添加的用户登录信息Dtos集合</param>
        /// <param name="cacheUser">当前操作用户的缓存</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> AddUserLogin(CacheUser cacheUser, params UserLoginInputDto[] inputDtos)
        {
            try
            {
                if (inputDtos.Length <= 0)
                {
                    return new OperationResult(OperationResultType.Error, "添加用户的数组不存在存数！");
                }
                else
                {
                    List<string> names = new List<string>();
                    UserLoginRepository.UnitOfWork.BeginTransaction();
                    foreach (UserLoginInputDto dto in inputDtos)
                    {
                        if (cacheUser.Level >= dto.Level)
                        {
                            return new OperationResult(OperationResultType.ValidError, $"用户:{dto.UserName}权限等级参数设置错误");
                        }
                        UserLogin value = dto.MapTo<UserLogin>();
                        value.SecretKey = new Random().NextLetterString(16);
                        value.Password = value.Password.AESEncrypt128(key: value.SecretKey);
                        value.UserMany.Add(new User
                        {
                            CreatedTime = DateTime.Now,
                            CreatorUserId = cacheUser.UserName,
                            LastUpdatedTime = DateTime.Now,
                            UserLoginOne = value,
                        });
                        await UserLoginRepository.InsertAsync(value);
                        if (value.Level == 2)
                        {
                            int count = UserOrganizeMapRepository.CheckExists(m => m.Organize_Id == value.Organize_Id && m.UserLogin_Id == value.Id) ? 0 : UserOrganizeMapRepository.Insert(new UserOrganizeMap { Organize_Id = value.Organize_Id, UserLogin_Id = value.Id });
                        }
                        names.Add(value.UserName);
                    }
                    UserLoginRepository.UnitOfWork.Commit();
                    if (names.Count == 0)
                    {
                        return new OperationResult(OperationResultType.NoChanged, "未能添加任何用户");
                    }
                    else
                    {
                        return new OperationResult(OperationResultType.Success, "用户:{0}创建成功".FormatWith(names.ExpandAndToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"id:{ex.InnerException.Message}");
            }
        }

        #endregion

        #region 删除用户登陆信息
        /// <summary>
        /// 删除用户登录信息
        /// </summary>
        /// <param name="ids">要删除的登录信息</param>
        /// <param name="cache">当前操作的缓存用户</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteUserLogin(CacheUser cache, params Guid[] ids)
        {
            try
            {
                OperationResult result = UserLoginRepository.Delete(ids,
                 checkAction: entity => {
                     if (cache.IsAdministrator)
                     {
                         return;
                     }
                     if (!cache.Level.IsBetween(1, 2))
                     {
                         throw new Exception($"id:你没有权限进行该功能操作！");
                     }
                     var listId = from a in UserOrganizeMapRepository.Entities
                                  join b in UserLoginRepository.Entities on
                                  a.Organize_Id equals b.Organize_Id
                                  where a.UserLogin_Id == cache.Id
                                  select b.Id;
                     foreach (var i in ids)
                     {
                         if (!listId.Contains(i))
                         {
                             throw new Exception($"id:主键:{i}的用户信息不存在你的操作范围！,");
                         }
                     }
                 },
                 deleteFunc: entity =>
                  {
                      if (entity == null)
                      {
                          throw new Exception("id:准备删除的目标不存在");
                      }
                      HostPolicyLogRepository.Delete(m => m.UserLogin_Id == entity.Id);
                      return entity;
                  });
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"id:{ex.InnerException.Message}");
            }
        }

        #endregion

        #region 用户基本信息更新
        public OperationResult UpdatedUser(UserInputDto inputDto)
        {
            if (!UserLoginRepository.CheckExists(m => m.Id == inputDto.Id))
            {
                throw new Exception("id:用户信息不存在");
            }
            else
            {
                var result = UserLoginRepository.TrackEntities.First(m => m.Id == inputDto.Id);
                var result_user = result.UserMany.First();
                result_user.Email = inputDto.Email;
                result_user.PhoneNumber = inputDto.PhoneNumber;
                result_user.WeChat = inputDto.WeChat;
                result_user.Remark = inputDto.Remark;
                result_user.Theme = inputDto.Theme;
                result_user.RealName = inputDto.RealName;
                result_user.NickName = inputDto.NickName;
                result_user.Sex = inputDto.Sex;
                result_user.IsAlarm = inputDto.IsAlarm;
                result_user.IsSysReceive = inputDto.IsSysReceive;
                result_user.Language = inputDto.Language;
                result_user.LastUpdatedTime = DateTime.Now;
                result_user.LastUpdatorUserId = result.UserName;
                int n = UserLoginRepository.Update(result);
                if (n > 0)
                {
                    return new OperationResult(OperationResultType.Success, "更新用户基本信成功!");
                }
                else
                {
                    throw new Exception("id:更新用户数据失败");

                }
            }
        }

        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改月用户密码
        /// </summary>
        /// <param name="id">用户登录ID</param>
        /// <param name="od">旧密码</param>
        /// <param name="nd">新密码</param>
        /// <returns></returns>
        public OperationResult ChangePasswrod(Guid id, string od, string nd)
        {
            if (!UserLoginRepository.CheckExists(m => m.Id == id))
            {
                throw new Exception("id:未查询到用户信息");
            }
            else
            {
                var result = UserLoginRepository.TrackEntities.First(m => m.Id == id);
                if (result.Password != od.AESEncrypt128(result.SecretKey))
                {
                    throw new Exception("id:验证旧密码出错");
                }
                else
                {
                    result.SecretKey = new Random().NextLetterString(16).ToUpper();
                    result.Password = nd.AESEncrypt128(result.SecretKey);
                    if (UserLoginRepository.Update(result) > 0)
                    {
                        //修改密码后移除登录缓存
                        ICache iCache = CacheManager.GetCacher<CacheUser>();
                        iCache.Remove(result.UserName.AESEncrypt128());
                        return new OperationResult(OperationResultType.Success, $"修改用户{result.UserName}密码成功");
                    }
                    else
                    {
                        throw new Exception($"id:修改用户{result.UserName}密码失败");
                    }
                }
            }
        }
        #endregion

        #region 获取用户管理的组织机构
        /// <summary>
        /// 获取用户管理的组织机构
        /// </summary>
        /// <param name="id">当前操作的用户主键</param>
        /// <returns></returns>
        public List<UserOrganizeMapOutDto> GetUserMaptoOrganize(Guid id)
        {
            List<UserOrganizeMapOutDto> listIn = new List<UserOrganizeMapOutDto>();
            var result = UserOrganizeMapRepository.GetInclude(m => m.OrganizeOne).Where(m => m.UserLogin_Id == id).ToList();
            foreach (var i in result)
            {
                listIn.Add(i.MapTo<UserOrganizeMapOutDto>());
            }
            return listIn;
        }

        #endregion

        #region 查询计算源
        /// <summary>
        /// 获取用户登录信息查询集合
        /// </summary>
        public IQueryable<UserLogin> IQUserLogins => UserLoginRepository.Entities;

        public IQueryable<User> IQUsers => UserRepository.Entities;
        #endregion

        #region 基于获取的用户列表信息修改

        /// <summary>
        /// 基于获取的用户列表信息修改
        /// </summary>
        /// <param name="users">待修改用户的集合</param>
        /// <returns></returns>
        public async Task<OperationResult> EditUserData(params UserPageIn[] users)
        {
            if (users.Length <= 0)
            {
                return new OperationResult(OperationResultType.Error, "待修改的用户数组不存在存数！");
            }
            UserLoginRepository.UnitOfWork.BeginTransaction();
            List<string> names = new List<string>();
            foreach (var i in users)
            {
                var user = UserLoginRepository.TrackEntities.Where(m => m.Id == i.Id).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception($"id:主键为:{i.Id}用户信息不存在");
                }
                user = i.MapTo(user);
                var user_result = user.UserMany.FirstOrDefault();
                user_result.NickName = i.NickName;
                user_result.PhoneNumber = i.PhoneNumber;
                user_result.RealName = i.RealName;
                user_result.Email = i.Email;
                user_result.LastUpdatorUserId = i.LastUpdatorUserId;
                user_result.LastUpdatedTime = DateTime.Now;
                await UserLoginRepository.UpdateAsync(user);
                names.Add(i.UserName);
            }
            UserLoginRepository.UnitOfWork.Commit();
            if (names.Count == 0)
            {
                return new OperationResult(OperationResultType.NoChanged, "未能修改任何数据");
            }
            else
            {
                return new OperationResult(OperationResultType.Success, "用户:{0}的数据修改成功".FormatWith(names.ExpandAndToString()));
            }
        }
        #endregion

        #region 标记信息为已读
        /// <summary>
        /// 标记信息为已读
        /// </summary>
        /// <param name="Ids">准备要标记为已读的信息主键</param>
        /// <returns></returns>
        public OperationResult MarkRead(params Guid[] Ids)
        {
            int count = 0;
            InformationRepository.UnitOfWork.BeginTransaction();
            foreach (var id in Ids)
            {
                var result = InformationRepository.TrackEntities.FirstOrDefault(m => m.Id == id);
                if (result == null)
                {
                    throw new Exception($"id:主键为{id}的用户提示信息不存在");
                }
                result.IsReaded = true;
                count += InformationRepository.Update(result);
            }
            InformationRepository.UnitOfWork.Commit();
            if (count > 0)
            {
                return new OperationResult(OperationResultType.Success, $"{count}条信息已被标记为已读！");
            }
            else
            {
                return new OperationResult();
            }
        }
        #endregion

        #region 删除指定的用户提示信息
        /// <summary>
        /// 删除指定的用户提示信息
        /// </summary>
        /// <param name="Ids">要删除的信息主键集合</param>
        /// <returns></returns>
        public OperationResult DeleteInformation(params Guid[] Ids)
        {
            int count = 0;
            foreach (var id in Ids)
            {
                count += InformationRepository.DeleteDirect(id);
            }
            if (count > 0)
            {
                return new OperationResult(OperationResultType.Success, $"{count}条信息已被删除！");
            }
            else
            {
                return new OperationResult();
            }
        }
        #endregion

        #region 添加警报信息
        /// <summary>
        /// 添加警报信息
        /// </summary>
        /// <param name="dynamics">警报信息集合</param>
        /// <returns></returns>
        public OperationResult AddAlert(params dynamic[] dynamics)
        {
            int row = 0;
            InformationRepository.UnitOfWork.BeginTransaction();
            foreach (dynamic dy in dynamics)
            {
                /* * * * * *
                 * 1	超压	            超过报警电压
                 * 2	欠压	            低于报警电压
                 * 3	开门检测	        霍尔传感器
                 * 4	分机功率异常	    比对实际功率与额定功率
                 * 5	主机断线	        断线时间超过预设的报警时间
                 * 6	分机断线	        断线时间超过预设的报警时间
                 * 7	运行日志	        存储所有操作日志
                 * 8	主机功率异常	    回路检测
                 * * * * * */

                int in_type = dy.InType;
                string repack = dy.RegPack;
                int subnum = dy.SubNum;
                switch (in_type)
                {
                    #region 主机
                    // 超压 Index = 1
                    case 1:                     
                        row += SetHostAlert(repack, 1);
                        break;
                    // 欠压 Index = 2
                    case 2:
                        row += SetHostAlert(repack, 2);
                        break;
                    // 开门检测 Index = 3
                    case 3: 
                        row += SetHostAlert(repack, 3);
                        break;
                    // 主机功率异常 Index =4
                    case 8:
                        row += SetHostAlert(repack, 4);
                        break;
                    // 主机断线 Index = 0
                    case 5:
                        row += SetHostAlert(repack, 0);
                        break;
                    #endregion

                    #region 分控
                    // 分控断线 Index =11
                    case 6:
                        row += SetSubAlert(repack, 11, subnum);
                        break;
                    // 分控功率异常 Index = 12
                    case 4:
                        row += SetSubAlert(repack, 4, subnum);
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            InformationRepository.UnitOfWork.Commit();
            return new OperationResult( OperationResultType.Success,$"添加警报信息[{row}]条");
        }

        /// <summary>
        /// 设置主机警报信息
        /// </summary>
        /// <param name="repack">主机注册包</param>
        /// <param name="index">警报类型序号</param>
        /// <returns></returns>
        private int SetHostAlert(string repack,int index)
        {
            int row = 0;
            // 获取目标主机信息
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == repack);

            if (index == 0)
            {   //主机断线时更新主机在线状态
                HostRealTimeData hostReal = HostRealTimeDataRepository.TrackEntities.FirstOrDefault(m => m.Host_Id == host.Id);
                hostReal.IsOnline = false;
                HostRealTimeDataRepository.Update(hostReal);
            }
            // 获取警报类型字典信息(超压 Index = 1)
            var ditem = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "InformationType" && m.Index == index);

            if (host != null && ditem != null)
            {
                // 获取管理该组织机构的用户信息
                var user_orgs = UserOrganizeMapRepository.Entities.Where(m => m.Organize_Id == host.Organize_Id).ToList();
                if (user_orgs != null)
                {
                    foreach (var user in user_orgs)
                    {
                        row += InformationRepository.Insert(new Information
                        {
                            CreatedTime = DateTime.Now,
                            DataItemDetail_Id = ditem.Id,
                            IsReaded = false,
                            ObjectId = host.Id,
                            Organize_Id = host.Organize_Id,
                            TypeIndex = ditem.Index,
                            TypeName = ditem.FullName,
                            UserLogin_Id = user.UserLogin_Id
                        });
                    }
                }
            }
            return row;
        }

        /// <summary>
        /// 设置分控警报信息
        /// </summary>
        /// <param name="repack">主机注册包</param>
        /// <param name="index">警报类型序号</param>
        /// <param name="subnum">分控编号</param>
        /// <returns></returns>
        private int SetSubAlert(string repack, int index, int subnum)
        {
            int row = 0;
            // 获取目标主机信息
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == repack);
            // 获取警报类型字典信息(超压 Index = 1)
            var ditem = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "InformationType" && m.Index == index);

            if (host != null && ditem != null)
            {
                // 获取管理该组织机构的用户信息
                var user_orgs = UserOrganizeMapRepository.Entities.Where(m => m.Organize_Id == host.Organize_Id).ToList();
                var sub = SubControlRepository.Entities.FirstOrDefault(m=>m.LigthPoleOne.Host_Id==host.Id&&m.SubNum==subnum);
                if (user_orgs != null && sub != null)
                {
                    foreach (var user in user_orgs)
                    {
                        row += InformationRepository.Insert(new Information
                        {
                            CreatedTime = DateTime.Now,
                            DataItemDetail_Id = ditem.Id,
                            IsReaded = false,
                            ObjectId = sub.Id,
                            Organize_Id = host.Organize_Id,
                            TypeIndex = ditem.Index,
                            TypeName = ditem.FullName,
                            UserLogin_Id = user.UserLogin_Id
                        });
                    }
                }
            }

            return row;
        }
        #endregion
        #endregion
    }
}
