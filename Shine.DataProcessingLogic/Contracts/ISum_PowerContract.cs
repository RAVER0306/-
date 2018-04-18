/*===================================================
* 接口名称: ISum_PowerContract
* 接口描述: 查询能耗统计接口
* 创 建 人: myining
* 创建时间: 2018/3/2 9:45:48
* 修 改 人: 
* 修改时间:
* 修改原因:
* 版    本：version 1.0
=====================================================*/
using Shine.Comman.Data;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Contracts
{
    public interface ISum_PowerContract: IScopeDependency
    {
        /// <summary>
        /// 年能耗查询数据集
        /// </summary>
        IQueryable<AnnualElectricity> AnnualQueryable { get; }

        /// <summary>
        /// 月能耗查询数据集
        /// </summary>
        IQueryable<MonthElectricity> MonthQueryable { get; }

        /// <summary>
        /// 天能耗查询数据集
        /// </summary>
        IQueryable<DayElectricity> DayQueryable { get; }

        /// <summary>
        /// 按月统计能耗
        /// </summary>
        /// <param name="DataItemDetailId">能耗类型主键</param>
        /// <param name="OrganizeId">查询指定组织机构</param>
        /// <param name="Year">统计指定年的能耗信息</param>
        /// <returns></returns>
        OperationResult Sum_MonthPower(Guid DataItemDetailId, Guid OrganizeId,int Year);

        /// <summary>
        /// 按天统计能耗
        /// </summary>
        /// <param name="DataItemDetailId">能耗类型</param>
        /// <param name="OrganizeId">指定的组织机构</param>
        /// <param name="Month">指定月</param>
        /// <returns></returns>
        OperationResult Sum_DayPower(Guid DataItemDetailId, Guid OrganizeId, int Year, int Month);

        /// <summary>
        /// 按小时统计能耗
        /// </summary>
        /// <param name="DataItemDetailId">能耗类型</param>
        /// <param name="OrganizeId">指定组织</param>
        /// <param name="day">指定月份</param>
        /// <returns></returns>
        OperationResult Sum_HourPower(Guid DataItemDetailId, Guid OrganizeId, int Year, int Month, int day);
    }
}
