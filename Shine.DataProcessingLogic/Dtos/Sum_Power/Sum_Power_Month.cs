/*===================================================
* 类名称: Sum_Power_Month
* 类描述: 记载按月能耗统计信息的实体
* 创建人: 72440
* 创建时间: 2018/3/2 9:59:09
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using Shine.Core.Data;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.Dtos.Sum_Power
{
    public class Sum_Power_Month
    {
        private IRepository<DataItemDetail, Guid> repository;
        public void SetRepository(IRepository<DataItemDetail, Guid> RepositoryDataItemDetail)
        {
            repository = RepositoryDataItemDetail;
        }


        /// <summary>
        /// 指定机构下指定能耗类型的1月总能耗
        /// </summary>
        public double Month_Sum1 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的2月总能耗
        /// </summary>
        public double Month_Sum2 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的3月总能耗
        /// </summary>
        public double Month_Sum3 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的4月总能耗
        /// </summary>
        public double Month_Sum4 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的5月总能耗
        /// </summary>
        public double Month_Sum5 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的6月总能耗
        /// </summary>
        public double Month_Sum6 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的7月总能耗
        /// </summary>
        public double Month_Sum7 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的8月总能耗
        /// </summary>
        public double Month_Sum8 { set; get; }


        /// 指定机构下指定能耗类型的9月总能耗
        /// </summary>
        public double Month_Sum9 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的10月总能耗
        /// </summary>
        public double Month_Sum10 { set; get; }

        /// 指定机构下指定能耗类型的11月总能耗
        /// </summary>
        public double Month_Sum11 { set; get; }

        /// <summary>
        /// 指定机构下指定能耗类型的12月总能耗
        /// </summary>
        public double Month_Sum12 { set; get; }

        /// <summary>
        /// 指定统计的年
        /// </summary>
        public int Year { set; get; }

        /// <summary>
        /// 能耗类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 能耗类型内容
        /// </summary>
        public DataItemDetail GetItemDetail => repository.GetByKey(DataItemDetail_Id);

        /// <summary>
        /// 指定年累计总能耗
        /// </summary>
        public double YearTotal_Sum { set; get; }

        /// <summary>
        /// 所有设备历史累计总能耗
        /// </summary>
        public double Cumulative_Sum { set; get; }
    }
}
