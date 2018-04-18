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
    public class UserBlackListService : IUserBlackListContract
    {
        #region 数据仓储
        public IRepository<UserBlackList, Guid> UserBlackListRepository { set; get; }
        #endregion

        #region 接口实现
        public IQueryable<UserBlackList> UBLQueryable => UserBlackListRepository.Entities;

        /// <summary>
        /// 编辑黑名单
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待更改的数据集合</param>
        /// <returns></returns>
        public OperationResult EditUserBlackLists(CacheUser cache, params UserBlackListInputDto[] datas) => UserBlackListRepository.Update(datas,
            checkAction: (dto, Entity) =>
            {
                if (UserBlackListRepository.CheckExists(m => m.UserLogin_Id == cache.Id && m.DataItemDetail_Id == dto.DataItemDetail_Id&&m.Id!=dto.Id))
                {
                    throw new Exception($"id:请求更新的:{dto.FullName} 的黑名单类型已经存在，每种黑名单类型都是唯一的!");
                }
            },
            updateFunc: (dto, entity) =>
            {               
                return entity;
            });


        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        public OperationResult AddUserBlackLists(CacheUser cache, params UserBlackListInputDto[] datas) => UserBlackListRepository.Insert(datas,
            checkAction: dto =>
             {
                if(UserBlackListRepository.CheckExists(m=>m.UserLogin_Id==cache.Id&&m.DataItemDetail_Id==dto.DataItemDetail_Id))
                 {
                     throw new Exception($"id:请求添加的:{dto.FullName} 的黑名单类型已经存在，每种黑名单类型都是唯一的!");
                 }
             },
            updateFunc: (dto, entity) =>
             {
                 entity.UserLogin_Id = cache.Id;
                 entity.CreatedTime = DateTime.Now;
                 return entity;
             });

        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        public OperationResult DeleteUserBlackLists(CacheUser cache, params Guid[] Ids)
        {
            int count = 0;
            UserBlackListRepository.UnitOfWork.BeginTransaction();
            foreach (var id in Ids)
            {
                var value = UserBlackListRepository.Entities.FirstOrDefault(m => m.Id == id);
                if (value == null)
                {
                    throw new Exception($"id:待删除的黑名单Id：{id} 不能存在");
                }
                else if (value != null && value.UserLogin_Id != cache.Id && !cache.IsAdministrator)
                {
                    throw new Exception($"id:你的权限不允许删除主键:{id}的黑名单");
                }
                else
                {
                    count += UserBlackListRepository.DeleteDirect(id);
                }
            }
            UserBlackListRepository.UnitOfWork.Commit();
            if (count > 0)
            {
                return new OperationResult( OperationResultType.Success,$"成功删除{count}条数据");
            }
            else
            {
                return new OperationResult();
            }
        }
        #endregion
    }
}
