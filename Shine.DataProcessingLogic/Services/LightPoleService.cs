using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Data;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Services
{
    public class LightPoleService : ILightPoleContract
    {
        #region 数据仓储
        /// <summary>
        /// <see cref="LightPole"/>表数据仓储
        /// </summary>
        public IRepository<LightPole, Guid> LightPoleRepository { set; get; }

        /// <summary>
        /// 主机基本信息表<see cref="Host"/>数据仓储对象
        /// </summary>
        public IRepository<Host, Guid> HostRepository { set; get; }

        /// <summary>
        /// <see cref="SubAggregation"/> 表的数据仓储
        /// </summary>
        public IRepository<SubAggregation,Guid> SubAggregationRepository { set; get; }

        #endregion

        #region 接口实现
        /// <summary>
        /// <see cref="LightPole"/>表预查询数据
        /// </summary>
        public IQueryable<LightPole> LightPoleQueryable => LightPoleRepository.Entities;

        /// <summary>
        /// 添加灯杆信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        public OperationResult AddLightPoles(CacheUser cache, params LightPoleInputDto[] datas) => LightPoleRepository.Insert(datas,
                checkAction: dto => 
                {
                },
                updateFunc: (dto, entity) =>
                 {
                     //var host = HostRepository.Entities.FirstOrDefault(m => m.Id == dto.Host_Id);
                     //if (host == null)
                     //{
                     //    throw new Exception("id:未找到关联的主机Id");
                     //}
                     //SubAggregation v1 = new SubAggregation
                     //{
                     //    Host_Id = dto.Host_Id,
                     //    LightPole_Id = entity.Id,
                     //    Organzie_Id = host.Organize_Id,
                     //    CreatedTime = DateTime.Now
                     //};
                     //SubAggregationRepository.Insert(v1);
                     entity.CreatedTime = DateTime.Now;
                     return entity;
                 });

        /// <summary>
        /// 编辑灯杆信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        public OperationResult EditLightPoles(CacheUser cache, params LightPoleInputDto[] datas)
        {
            return LightPoleRepository.Update(datas,
                checkAction: (dto, entity) => 
                {
                },
                updateFunc: (dto, entity) => 
                {
                    //var v1 = SubAggregationRepository.Entities.FirstOrDefault(m => m.LightPole_Id == dto.Id);                  
                    //if (v1.Host_Id != dto.Host_Id)
                    //{
                    //    var host = HostRepository.Entities.FirstOrDefault(m => m.Id == dto.Host_Id);
                    //    if (host == null) { throw new Exception("id:更改数据关联的主机主键不存在"); }
                    //    v1.Host_Id = host.Id;
                    //    v1.Organzie_Id = host.Organize_Id;
                    //    SubAggregationRepository.Update(v1);
                    //}
                    return entity;
                });
        }

        /// <summary>
        /// 删除灯杆信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        public OperationResult DeleteLightPoles(CacheUser cache, params Guid[] Ids)
        {
            try
            {
                int count = 0;
                LightPoleRepository.UnitOfWork.BeginTransaction();
                foreach (var i in Ids)
                {
                    count += LightPoleRepository.DeleteDirect(i);
                }
                LightPoleRepository.UnitOfWork.Commit();
                if (count > 0)
                {
                    return new OperationResult( OperationResultType.Success,$"删除{count}数据成功");
                }
                else
                {
                    return new OperationResult();
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"执行出现错误:{ex.Message}");
            }
        }
        #endregion
    }
}
