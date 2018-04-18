using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.HostManager.Out;
using Shine.DataProcessingLogic.Models.HostManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface IHostContract : IScopeDependency
    {
        /// <summary>
        /// 获取主机实时数据查询
        /// </summary>
        IQueryable<HostRealTimeData> HostRealTimeDataHostQueryable { get; }

        /// <summary>
        /// 获取主机查询操作集合
        /// </summary>
        IQueryable<Host> HostQueryable { get; }

        /// <summary>
        ///<see cref="HostLoginQueryable"/> 查询操作集合
        /// </summary>
        IQueryable<HostLogin> HostLoginQueryable { get; }

        /// <summary>
        ///<see cref="HostLoginQueryable"/> 查询操作集合
        /// </summary>
        IQueryable<HostParameter> HostParameterQueryable { get; }

        /// <summary>
        /// 添加新的主机信息
        /// </summary>
        /// <param name="cache">当前登录用户的缓存</param>
        /// <param name="belongOrganizeId">当前用户所管理的组织机构ID</param>
        /// <param name="dtos">输入主机信息实体</param>
        /// <returns></returns>
        OperationResult AddHosts(CacheUser cache,Guid[]belongOrganizeId,params HostInputDto[] dtos);

        /// <summary>
        /// 修改主机基本信
        /// </summary>
        /// <param name="cachePageId">当前用户获取的主机列表缓存页主键</param>
        /// <param name="dtos">输入信息实体模型</param>
        /// <returns></returns>
        OperationResult EditHosts(Guid[] cachePageId, params HostInputDto[] dtos);

        /// <summary>
        /// 删除用户已获取的主机列表中的数据
        /// </summary>
        /// <param name="cachePageId">当前用户获取的主机列表缓存页主键</param>
        /// <param name="Ids">准备被删除的主键集合</param>
        /// <returns></returns>
        OperationResult DeleteHosts(Guid[] cachePageId, params Guid[] Ids);

        /// <summary>
        /// 更新主机实时数据
        /// </summary>
        /// <param name="datas">待更新的数据</param>
        /// <returns></returns>
        string UpdatedHostTimeDatas(params HostRealTimeDataInputDto[] datas);

        /// <summary>
        /// 主机系统参数更新
        /// </summary>
        /// <param name="_0X25_In">数据</param>
        /// <returns></returns>
        OperationResult UpdatedHostParameter_0x25(HostParameter_0x25_In _0X25_In);

        /// <summary>
        /// 主机登陆信息数据更新
        /// </summary>
        /// <param name="_0X44_In">数据</param>
        /// <returns></returns>
        OperationResult UpdatedHostLogin_0x44(HostLogin_0x44_In _0X44_In);

        /// <summary>
        /// 获取指定组织机构下的所有主机
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        OperationResult GetHostCount(params Guid[] OrganizeId);

        /// <summary>
        /// 更新主机应答信息
        /// </summary>
        /// <param name="_0X61_In">数据</param>
        /// <returns></returns>
        OperationResult UpdatedHost_0x61(Host_0x61_In _0X61_In);

        List<EmailHostOffines> GetOffineHosts();
    }
}
