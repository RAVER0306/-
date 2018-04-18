/*===================================================
* 类名称: Sum_PowerService
* 类描述:
* 创建人: 72440
* 创建时间: 2018/3/2 9:47:40
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using Shine.Comman.Data;
using Shine.Core.Data;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos.Sum_Power;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Services
{
    public class Sum_PowerService: ISum_PowerContract
    {
        #region 注入仓储
        /// <summary>
        /// 年能耗数据仓储
        /// </summary>
        public IRepository<AnnualElectricity, Guid> AnnualElectricityRepository { set; get; }

        /// <summary>
        /// 月能耗数据仓储
        /// </summary>

        public IRepository<MonthElectricity, Guid> MonthElectricityRepository { set; get; }

        /// <summary>
        /// 天能耗数据仓储
        /// </summary>
        public IRepository<DayElectricity, Guid> DayElectricityRepository { set; get; }
        
        public IRepository<DataItemDetail, Guid> RepositoryDataItemDetail { set; get; }
        #endregion

        #region 数据查询集
        /// <summary>
        /// 年能耗查询数据集
        /// </summary>
        public IQueryable<AnnualElectricity> AnnualQueryable => AnnualElectricityRepository.Entities;

        /// <summary>
        /// 月能耗查询数据集
        /// </summary>
        public IQueryable<MonthElectricity> MonthQueryable => MonthElectricityRepository.Entities;

        /// <summary>
        /// 天能耗查询数据集
        /// </summary>
        public IQueryable<DayElectricity> DayQueryable => DayElectricityRepository.Entities;
        #endregion

        /// <summary>
        /// 按月统计能耗
        /// </summary>
        /// <param name="DataItemDetailId">能耗类型主键</param>
        /// <param name="OrganizeId">查询指定组织机构</param>
        /// <param name="Year">统计指定年的能耗信息</param>
        /// <returns></returns>
        public OperationResult Sum_MonthPower(Guid DataItemDetailId, Guid OrganizeId, int Year)
        {
            try
            {
                var param = new SqlParameter[]
                {
                   new SqlParameter("@DataItemDetailId", DataItemDetailId),
                   new SqlParameter("@ThisYear", Year),
                   new SqlParameter("@OrganizeId", OrganizeId)
                };
                var result = AnnualElectricityRepository.UnitOfWork.SqlQuery<Sum_Power_Month>("EXEC Sp_SumYearPower @DataItemDetailId,@ThisYear,@OrganizeId", param);
                if (result == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, $"不存在数据");
                }
                else
                {
                    var data = result.FirstOrDefault();
                    data.SetRepository(RepositoryDataItemDetail);
                    return new OperationResult(OperationResultType.Success, $"数据请求成功", data);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"id:查询能耗信息出错,标记：{ex.Message}");
            }
        }

        /// <summary>
        /// 按天统计能耗
        /// </summary>
        /// <param name="DataItemDetailId">能耗类型</param>
        /// <param name="OrganizeId">指定的组织机构</param>
        /// <param name="Month">指定月</param>
        /// <returns></returns>
        public OperationResult Sum_DayPower(Guid DataItemDetailId, Guid OrganizeId, int Year, int Month)
        {
            try
            {
                var param = new SqlParameter[]
                {
                   new SqlParameter("@Month", Month),
                   new SqlParameter("@OrganizeId", OrganizeId),
                   new SqlParameter("@ItemId", DataItemDetailId),
                   new SqlParameter("@Year",Year)
                };
                var result = AnnualElectricityRepository.UnitOfWork.SqlQuery<Sum_Power_Day>("EXEC Sp_SumMonthPower @Month,@OrganizeId,@ItemId,@Year", param);
                if (result == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, $"不存在数据");
                }
                else
                {
                    var data = result.FirstOrDefault();
                    data.SetRepository(RepositoryDataItemDetail);
                    return new OperationResult(OperationResultType.Success, $"数据请求成功", data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"id:查询能耗信息出错,标记：{ex.Message}");
            }
        }

        /// <summary>
        /// 按小时统计能耗
        /// </summary>
        /// <param name="DataItemDetailId">能耗类型</param>
        /// <param name="OrganizeId">指定组织</param>
        /// <param name="day">指定月份</param>
        /// <returns></returns>
        public OperationResult Sum_HourPower(Guid DataItemDetailId, Guid OrganizeId,int Year, int Month, int day)
        {
            try
            {
                var param = new SqlParameter[]
                {
                   new SqlParameter("@Day", day),
                   new SqlParameter("@OrganizeId", OrganizeId),
                   new SqlParameter("@ItemId", DataItemDetailId),
                   new SqlParameter("@Year",Year),
                   new SqlParameter("@Month",Month)
                };
                var result = AnnualElectricityRepository.UnitOfWork.SqlQuery<Sum_Power_Hour>("EXEC Sp_SumDayPower @Day,@OrganizeId,@ItemId,@Year,@Month", param).ToList();
                if (result == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, $"不存在数据");
                }
                else
                {
                    var data = result.FirstOrDefault();
                    data?.SetRepository(RepositoryDataItemDetail);
                    return new OperationResult(OperationResultType.Success, $"数据请求成功", data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"id:查询能耗信息出错,标记：{ex.Message}");
            }
        }
    }
}
