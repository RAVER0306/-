using Shine.DataProcessingLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.Core.Data;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Models.HostManager;

namespace Shine.DataProcessingLogic.Services
{
    public class GroupControlService : IGroupControlContract
    {
        #region 数据仓储
        /// <summary>
        /// 分组数据仓储
        /// </summary>
        public IRepository<GroupControl, Guid> GroupControlRepository { set; get; }

        public IRepository<Host, Guid> HostRepository { set; get; }

        public IRepository<DataItemDetail, Guid> DataItemDetailRepository { set; get; }
        #endregion

        #region 接口实现

        /// <summary>
        /// <see cref="GroupControl"/>表预查询数据
        /// </summary>      
        public IQueryable<GroupControl> GroupControlQueryable => GroupControlRepository.Entities;

        /// <summary>
        /// 添加分组信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        public OperationResult AddGroupControls(CacheUser cache, params GroupControlInputDto[] datas) =>
            GroupControlRepository.Insert(datas,
                checkAction:dto=> 
                {
                    if (GroupControlRepository.CheckExists(m => m.ObjectId == dto.ObjectId && m.Organzie_Id == dto.Organzie_Id && m.GrounpNum == dto.GrounpNum && m.DataItemDetail_Id == dto.DataItemDetail_Id))
                    {
                        throw new Exception($"id:该分组已经存在!"); 
                    }
                },
                updateFunc:(dto,entity)=> 
                {
                    entity.CreatedTime = DateTime.Now;
                    entity.UpdateTime = DateTime.Now;
                    return entity;
                });

        /// <summary>
        /// 编辑分组信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        public OperationResult EditGroupControls(CacheUser cache, params GroupControlInputDto[] datas) => GroupControlRepository.Update(datas,
            checkAction: (dto, entity) => 
            {
                if (dto.DataItemDetail_Id == entity.DataItemDetail_Id&&dto.GrounpNum!=entity.GrounpNum)
                {
                    if(GroupControlRepository.CheckExists(m=>m.GrounpNum==dto.GrounpNum&&m.ObjectId==dto.ObjectId&&m.Organzie_Id==dto.Organzie_Id))
                    {
                        throw new Exception($"id:更新数据失败，因为已经存在相同的类型数据");
                    }
                }
            },
            updateFunc: (dto,entity)=> 
            {
                return entity;
            });

        /// <summary>
        /// 删除分组信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        public OperationResult DeleteGroupControls(CacheUser cache, params Guid[] Ids)
        {
            try
            {
                int count = 0;
                GroupControlRepository.UnitOfWork.BeginTransaction();
                foreach (var id in Ids)
                {
                    count += GroupControlRepository.DeleteDirect(id);
                }
                GroupControlRepository.UnitOfWork.Commit();
                return count > 0 ? new OperationResult(OperationResultType.Success, $"成功删除{count}条数据") :
                    new OperationResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"id:出现错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新或添加来自主机的分组信息
        /// </summary>
        /// <param name="data">分组信息数据</param>
        /// <returns></returns>
        public OperationResult GroupControls_0x18(GroupControl_0x18_In data)
        {
            var host = HostRepository.Entities.FirstOrDefault(m => m.RegPackage == data.RegPackage);
            if (host == null)
            {
                return new OperationResult(OperationResultType.QueryNull, $"注册包：{data.RegPackage}的主机信息不存在");
            }
            else
            {
                var item = DataItemDetailRepository.Entities.FirstOrDefault(m => m.QueryCoding == "GroupType" && m.Index == 0);
                if (item == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, $"未能查找到分组类型主键");
                }
                var group = GroupControlRepository.TrackEntities.FirstOrDefault(m => m.ObjectId == host.Id && m.Organzie_Id == host.Organize_Id && m.GrounpNum == data.GrounpNum && m.DataItemDetail_Id == item.Id);

                if (group == null)
                {
                    //分组信息未存在，系统帮添加信息
                    group = data.MapTo<GroupControl>();
                    group.ObjectId = host.Id;
                    group.Organzie_Id = host.Organize_Id;
                    group.DataItemDetail_Id = item.Id;
                    group.GrounpName = $"HostGroups_{data.GrounpNum}";
                    group.CreatedTime = DateTime.Now;
                    group.UpdateTime = DateTime.Now;
                    return GroupControlRepository.Insert(group) > 0 ?
                        new OperationResult(OperationResultType.Success, $"更新主机：{data.RegPackage} 的分组：{data.GrounpNum}信息成功！") :
                        new OperationResult(OperationResultType.NoChanged, $"更新主机：{data.RegPackage} 的分组信息未发生任何改变");
                }
                else
                {
                    group.UpdateTime = DateTime.Now;
                    group.GroupContent = data.GroupContent;
                    return GroupControlRepository.Update(group) > 0 ?
                         new OperationResult(OperationResultType.Success, $"更新主机：{data.RegPackage} 的分组：{data.GrounpNum}信息成功！") :
                         new OperationResult(OperationResultType.NoChanged, $"更新主机：{data.RegPackage} 的分组信息未发生任何改变");
                }
            }
        }
        #endregion

    }
}
