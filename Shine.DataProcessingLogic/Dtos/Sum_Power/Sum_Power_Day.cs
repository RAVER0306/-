/*===================================================
* 类名称: Sum_Power_Day
* 类描述: 记载按天能耗统计信息的实体
* 创建人: myining
* 创建时间: 2018/3/2 15:22:21
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
    public class Sum_Power_Day
    {
        private IRepository<DataItemDetail, Guid> repository;
        public void SetRepository(IRepository<DataItemDetail, Guid> RepositoryDataItemDetail)
        {
            repository = RepositoryDataItemDetail;
        }
        /// <summary>
        /// 指定类型能耗指定月的第1天的总能耗
        /// </summary>
        public double Sum_D1 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第2天的总能耗
        /// </summary>
        public double Sum_D2 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第3天的总能耗
        /// </summary>
        public double Sum_D3 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第4天的总能耗
        /// </summary>
        public double Sum_D4 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第5天的总能耗
        /// </summary>
        public double Sum_D5 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第6天的总能耗
        /// </summary>
        public double Sum_D6 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第7天的总能耗
        /// </summary>
        public double Sum_D7 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第8天的总能耗
        /// </summary>
        public double Sum_D8 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第9天的总能耗
        /// </summary>
        public double Sum_D9 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第10天的总能耗
        /// </summary>
        public double Sum_D10 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第11天的总能耗
        /// </summary>
        public double Sum_D11 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第12天的总能耗
        /// </summary>
        public double Sum_D12 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第13天的总能耗
        /// </summary>
        public double Sum_D13 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第14天的总能耗
        /// </summary>
        public double Sum_D14 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第15天的总能耗
        /// </summary>
        public double Sum_D15 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第16天的总能耗
        /// </summary>
        public double Sum_D16 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第17天的总能耗
        /// </summary>
        public double Sum_D17 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第18天的总能耗
        /// </summary>
        public double Sum_D18 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第19天的总能耗
        /// </summary>
        public double Sum_D19 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第20天的总能耗
        /// </summary>
        public double Sum_D20 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第21天的总能耗
        /// </summary>
        public double Sum_D21 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第22天的总能耗
        /// </summary>
        public double Sum_D22 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第23天的总能耗
        /// </summary>
        public double Sum_D23 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第24天的总能耗
        /// </summary>
        public double Sum_D24 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第25天的总能耗
        /// </summary>
        public double Sum_D25 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第26天的总能耗
        /// </summary>
        public double Sum_D26 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第27天的总能耗
        /// </summary>
        public double Sum_D27 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第28天的总能耗
        /// </summary>
        public double Sum_D28 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第29天的总能耗
        /// </summary>
        public double Sum_D29 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第30天的总能耗
        /// </summary>
        public double Sum_D30 { set; get; }
        /// <summary>
        /// 指定类型能耗指定月的第31天的总能耗
        /// </summary>
        public double Sum_D31 { set; get; }

        /// <summary>
        /// 指定类型能耗指定月的总能耗
        /// </summary>
        public Double Sum_MonthTotal { set; get; }

        /// <summary>
        /// 指定月
        /// </summary>
        public int Month { set; get; }
        /// <summary>
        /// 能耗类型主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }
        /// <summary>
        /// 能耗类型信息
        /// </summary>
        public DataItemDetail GetItemDetail => repository.GetByKey(DataItemDetail_Id);
    }
}
