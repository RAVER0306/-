using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.HostManager.Out;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Shine.DataProcessingLogic.Contracts
{
    /// <summary>
    /// 分控服务接口
    /// </summary>
    public interface ISubControlContract : IScopeDependency
    {
        /// <summary>
        /// <see cref="SubControl"/>表预查询数据
        /// </summary>
        IQueryable<SubControl> SubControlQueryable { get; }

        /// <summary>
        /// 获取分控分控上的灯具信息
        /// </summary>
        IQueryable<SubRealTimeData> SubRealTimeDataQueryable { get; }
        
        /// <summary>
        /// <see cref="SubAggregation"/>表预查数据集合
        /// </summary>
        IQueryable<SubAggregation> SubAggregationQueryable { get; }

        /// <summary>
        /// 添加分控信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待添加的数据集合</param>
        /// <returns></returns>
        OperationResult AddSubControls(CacheUser cache, params SubControlInputDto[] datas);

        /// <summary>
        /// 编辑分控信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待编辑的数据集合</param>
        /// <returns></returns>
        OperationResult EditSubControls(CacheUser cache, params SubControlInputDto[] datas);

        /// <summary>
        /// 删除分控信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        OperationResult DeleteSubControls(CacheUser cache, params Guid[] Ids);

        /// <summary>
        /// 添加分控上的灯具基本信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <param name="datas">待添加的灯具</param>
        /// <returns></returns>
        OperationResult AddSubReadTimeDatas(CacheUser cache, params SubRealTimeDataInputDto[] datas);

        /// <summary>
        /// 编辑分控上的灯具基本信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存信息</param>
        /// <param name="datas">待添加的灯具</param>
        /// <returns></returns>
        OperationResult EditSubReadTimeDatas(CacheUser cache, params SubRealTimeDataInputDto[] datas);

        /// <summary>
        /// 删除分控上的灯具基本信息
        /// </summary>
        /// <param name="cache">当前操作用户的缓存</param>
        /// <param name="datas">待删除的数据Id集合</param>
        /// <returns></returns>
        OperationResult DeleteReadTimeDatas(CacheUser cache, params Guid[] Ids);

        /// <summary>
        /// 更新分控的实时数据
        /// </summary>
        /// <param name="data_0X16_In">待更新的数据</param>
        /// <returns></returns>
        string UpdateSubReadTimeData_0x16(params SubRealTimeData_0x16_In[] data_0X16_In);

        /// <summary>
        /// 编辑分控的经纬度
        /// </summary>
        /// <param name="Id">分控主键</param>
        /// <param name="Longitude">精度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        OperationResult EidtSubLocation(Guid Id, double Longitude, double Latitude);

        /// <summary>
        /// 更新分控的UID
        /// </summary>
        /// <param name="_0X29_In"></param>
        /// <returns></returns>
        OperationResult UpdatedHost_0x29(SubControl_0x29_In _0X29_In);

        /// <summary>
        /// 获取指定组织机构的分控数量
        /// </summary>
        /// <param name="Ids">指定的组织机构</param>
        /// <returns></returns>
        OperationResult GetSubCount(params Guid[] Ids);


        /// <summary>
        /// 获取指定组织机构的灯具数量
        /// </summary>
        /// <param name="Ids">指定的组织机构</param>
        /// <returns></returns>
        OperationResult GetLightCount(params Guid[] Ids);

        /// <summary>
        /// 获取掉线分控数据
        /// </summary>
        /// <returns></returns>
        List<EmailSubOffines> GetOffineSubs();
    }
}
