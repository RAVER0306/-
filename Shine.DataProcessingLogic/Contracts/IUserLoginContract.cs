using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.UserManager;
using Shine.DataProcessingLogic.Dtos.UserManager.In;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface IUserLoginContract : IScopeDependency
    {
        #region 用户登录信息业务
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="id">用户主键ID</param>
        /// <param name="imageFile">图片文件</param>
        /// <returns>是否修改成功</returns>
        Task<OperationResult> SetUserHeadIcon(Guid id,Task<FileInfo> imageFile);

        /// <summary>
        /// 获取用户头像byte[]
        /// </summary>
        /// <param name="id">用户主键</param>
        /// <returns></returns>
        byte[] GetHeadIconBits(Guid id);

        /// <summary>
        /// 获取<see cref="UserLogin"/>的数据源查询
        /// </summary>
        IQueryable<UserLogin> IQUserLogins { get; }

        IQueryable<Information> QueryableInfo { get; }

        /// <summary>
        /// 获取<see cref="User"/>的数据源查询
        /// </summary>
        IQueryable<User> IQUsers { get; }

        /// <summary>
        /// 用户进行登录检查
        /// </summary>
        /// <param name="UserName">用户帐号</param>
        /// <param name="Passwork">用户密码</param>
        /// <returns>用户登录成功的关联集合</returns>
        OperationResult CheckToUserLogin(string userName,string passwork);

        /// <summary>
        /// 添加用户登录信息
        /// </summary>
        /// <param name="inputDtos">要添加的用户登录信息Dtos集合</param>
        /// <param name="level">用户操作等级</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> AddUserLogin(CacheUser cacheUser,params UserLoginInputDto[] inputDtos);

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="inputDto">输入dto</param>
        /// <returns></returns>
        OperationResult UpdatedUser(UserInputDto inputDto);

        /// <summary>
        /// 修改月用户密码
        /// </summary>
        /// <param name="id">用户登录ID</param>
        /// <param name="od">旧密码</param>
        /// <param name="nd">新密码</param>
        /// <returns></returns>
        OperationResult ChangePasswrod(Guid id, string od, string nd);

        /// <summary>
        /// 删除用户登录信息
        /// </summary>
        /// <param name="ids">要删除的登录信息</param>
        /// <param name="cache">当前操作的缓存用户</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult>DeleteUserLogin(CacheUser cache, params Guid[] ids);

        /// <summary>
        /// 获取用户管理的组织机构
        /// </summary>
        /// <param name="id">当前操作的用户主键</param>
        /// <returns></returns>
        List<UserOrganizeMapOutDto> GetUserMaptoOrganize(Guid id);

        /// <summary>
        /// 基于获取的用户列表信息修改
        /// </summary>
        /// <param name="users">待修改用户的集合</param>
        /// <returns></returns>
        Task<OperationResult> EditUserData(params UserPageIn[] users);

        /// <summary>
        /// 标记信息为已读
        /// </summary>
        /// <param name="Ids">准备要标记为已读的信息主键</param>
        /// <returns></returns>
        OperationResult MarkRead(params Guid[] Ids);

        /// <summary>
        /// 删除指定的用户提示信息
        /// </summary>
        /// <param name="Ids">要删除的信息主键集合</param>
        /// <returns></returns>
        OperationResult DeleteInformation(params Guid[] Ids);

        /// <summary>
        /// 添加警报信息
        /// </summary>
        /// <param name="dynamics">警报信息集合</param>
        /// <returns></returns>
        OperationResult AddAlert(params dynamic[] dynamics);
        #endregion
    }
}
