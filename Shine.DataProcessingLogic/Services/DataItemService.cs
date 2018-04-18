using Shine.DataProcessingLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.Core.Data;
using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.Comman.Extensions;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;

namespace Shine.DataProcessingLogic.Services
{
    public class DataItemService : IDataItemContract
    {
        #region 仓储实现
        /// <summary>
        /// <see cref="DataItem"/>表数据仓储实现
        /// </summary>
        public IRepository<DataItem, Guid> DataItemRepository { set; get; }

        /// <summary>
        /// <see cref="DataItemDetail"/>表数据仓储实现
        /// </summary>
        public IRepository<DataItemDetail, Guid> DataItemDetailRepository { set; get; }
        #endregion

        #region 接口实现
        /// <summary>
        /// 表<see cref="DataItem"/>的查询数据集
        /// </summary>
        public IQueryable<DataItem> DataItemQueryable => DataItemRepository.Entities;

        /// <summary>
        /// 表<see cref="DataItemDetail"/>的查询数据集
        /// </summary>
        public IQueryable<DataItemDetail> DataItemDetailQueryable => DataItemDetailRepository.Entities;

        /// <summary>
        /// 添加字典数据目录
        /// </summary>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        public OperationResult AddDataItems(params DataItemInputDto[] datas) => DataItemRepository.Insert(datas,
                 checkAction: dto =>
                 {
                     if (DataItemRepository.CheckExists(b => b.QueryCoding == dto.QueryCoding && b.FullName == dto.FullName))
                     {
                         throw new Exception($"id:字典:{dto.FullName}&{dto.QueryCoding}参数已经存在,请不要重复添加！");
                     }
                 },
                 updateFunc: (dto, entity) =>
                 {
                     entity.CreatedTime = DateTime.Now;
                     entity.IsSystem = false;
                     return entity;
                 });

        /// <summary>
        /// 根据主键集合删除字典目录
        /// </summary>
        /// <param name="Ids">待删除的字典目录主键集合</param>
        /// <returns></returns>
        public OperationResult DeleteDataItems(params Guid[] Ids)
        {
            DataItemRepository.UnitOfWork.BeginTransaction();
            int count = 0;
            foreach (var id in Ids)
            {
                var result = DataItemRepository.Entities.FirstOrDefault(m => m.Id == id);
                if (result != null)
                {
                    if (result.IsSystem)
                    {
                        throw new Exception($"id:系统预设的字典不能被删除");
                    }
                    count += DataItemRepository.DeleteDirect(id);
                }
            }
            DataItemRepository.UnitOfWork.Commit();
            if (count > 0)
            {
                return new OperationResult(OperationResultType.Success, $"id:成功删除了{count}条数据");
            }
            else
            {
                return new OperationResult();
            }
        }

        /// <summary>
        /// 编辑字典目录
        /// </summary>
        /// <param name="datas">待更新的字典目录</param>
        /// <returns></returns>
        public OperationResult EditDataItems(params DataItemInputDto[] datas) => DataItemRepository.Update(datas,
            checkAction: (dto, entity) => 
            {
                if (dto.QueryCoding != entity.QueryCoding && entity.IsSystem)
                {
                    throw new Exception("id:系统预设的字典目录查询码不允许修改!");
                }
            },
            updateFunc: (dto,entity) => 
            {
                return entity;
            });

        /// <summary>
        /// 添加字典内容
        /// </summary>
        /// <param name="datas">待添加的字典内容集合</param>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <returns></returns>
        public OperationResult AddDataItemDetails(CacheUser cache, DataItemDetailInputDto[] datas) => DataItemDetailRepository.Insert(datas,
                checkAction: dto =>
                {
                    if (!cache.IsAdministrator)
                    {
                        var r1 = DataItemRepository.Entities.FirstOrDefault(b => b.Id == dto.DataItem_Id);
                        if (r1 == null)
                        {
                            throw new Exception($"id:待添加的:{dto.FullName} 字典内容所关联的目录不存在！");
                        }
                        else if (r1 != null &&! r1.IsPublic)
                        {
                            throw new Exception($"id:待添加的:{dto.FullName} 字典内容所关联的目录不是你的操作权限范围！");
                        }
                    }
                    if (DataItemDetailRepository.CheckExists(mb => mb.FullName == dto.FullName&&mb.Organzie_Id==cache.Organize_Id))
                    {
                        throw new Exception($"id:待添加的:{dto.FullName} 字典内容已存在！");
                    }

                },
                updateFunc: (dto, entity) =>
                {
                    entity.CreatedTime = DateTime.Now;
                    if (cache.IsAdministrator)
                    {
                        entity.Organzie_Id = null;
                    }
                    else
                    {
                        entity.Organzie_Id = cache.Organize_Id;
                        entity.IsPublic = false;
                    }
                    entity.Index = (byte)(DataItemDetailRepository.Entities.Max(m => m.Index) + 1);
                    entity.IsSystem = false;
                    entity.QueryCoding = DataItemRepository.Entities.FirstOrDefault(m => m.Id == entity.DataItem_Id).QueryCoding;
                    return entity;
                });

        /// <summary>
        /// 编辑字典内容
        /// </summary>
        /// <param name="datas">待更新的字典内容集合</param>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <returns></returns>
        public OperationResult EditDataItemDetails(CacheUser cache, DataItemDetailInputDto[] datas) => DataItemDetailRepository.Update(datas,
                checkAction: (edit, updated) =>
                {
                    if (updated.IsSystem == true && !cache.IsAdministrator)
                    {
                        throw new Exception($"id:字典内容:{edit.FullName} 是系统预设内容你的权限不能修改!");
                    }
                    else if (DataItemDetailRepository.CheckExists(m => m.FullName == edit.FullName && m.Id != edit.Id&&m.Organzie_Id==cache.Organize_Id))
                    {
                        throw new Exception($"id:你已存在字典内容名称 为‘{edit.FullName}’ 的参数!");
                    }
                },
                updateFunc: (edit, updated) =>
                {              
                    return updated;
                });


        /// <summary>
        /// 删除字典内容
        /// </summary>
        /// <param name="datas">待删除的字典内容主键集合</param>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <returns></returns>
        public OperationResult DeleteDataItemDetails(CacheUser cache, params Guid[] Ids)
        {
            int count = 0;
            DataItemDetailRepository.UnitOfWork.BeginTransaction();
            foreach (var id in Ids)
            {
                var value1 = DataItemDetailRepository.Entities.FirstOrDefault(m => m.Id == id);
                if (value1 == null)
                {
                    throw new Exception($"id:准备删除的对象:{value1.FullName} 不存在");
                }
                if (value1!=null&& value1.IsSystem && !cache.IsAdministrator)
                {
                    throw new Exception($"id:你没有权限删除系统字典内容:{value1.FullName}");
                }
                try
                {
                   count += DataItemDetailRepository.DeleteDirect(id);
                }
                catch (Exception ex)
                {
                    throw new Exception($"id:删除字典内容对象id:{id} 出错，请先删除与其关联的信息后操作。具体信息:{ex.InnerException.Message}");
                }
            }
            DataItemDetailRepository.UnitOfWork.Commit();

            if (count > 0)
            {
                return new OperationResult( OperationResultType.Success,$"成功删除{count}个对象!");
            }
            else
            {
                return new OperationResult();
            }
        }
        #endregion
    }
}
