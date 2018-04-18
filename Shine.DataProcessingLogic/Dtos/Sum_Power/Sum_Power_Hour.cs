/*===================================================
* 类名称: Sum_Power_Hour
* 类描述: 记载按小时能耗统计信息的实体
* 创建人: myining
* 创建时间: 2018/3/2 17:11:57
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using Shine.Core.Data;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.Sum_Power
{
    public class Sum_Power_Hour
    {
        private IRepository<DataItemDetail, Guid> repository;
        public void SetRepository(IRepository<DataItemDetail, Guid> RepositoryDataItemDetail)
        {
            repository = RepositoryDataItemDetail;
        }

        /// <summary>
        /// 指定类型能耗指定天的第1小时的总能耗
        /// </summary>
        public double Sum_H1 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第2小时的总能耗
        /// </summary>
        public double Sum_H2 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第3小时的总能耗
        /// </summary>
        public double Sum_H3 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第4小时的总能耗
        /// </summary>
        public double Sum_H4 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第5小时的总能耗
        /// </summary>
        public double Sum_H5 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第6小时的总能耗
        /// </summary>
        public double Sum_H6 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第7小时的总能耗
        /// </summary>
        public double Sum_H7 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第8小时的总能耗
        /// </summary>
        public double Sum_H8 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第9小时的总能耗
        /// </summary>
        public double Sum_H9 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第10小时的总能耗
        /// </summary>
        public double Sum_H10 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第11小时的总能耗
        /// </summary>
        public double Sum_H11 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第12小时的总能耗
        /// </summary>
        public double Sum_H12 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第13小时的总能耗
        /// </summary>
        public double Sum_H13 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第14小时的总能耗
        /// </summary>
        public double Sum_H14 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第15小时的总能耗
        /// </summary>
        public double Sum_H15 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第16小时的总能耗
        /// </summary>
        public double Sum_H16 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第17小时的总能耗
        /// </summary>
        public double Sum_H17 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第18小时的总能耗
        /// </summary>
        public double Sum_H18 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第19小时的总能耗
        /// </summary>
        public double Sum_H19 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第20小时的总能耗
        /// </summary>
        public double Sum_H20 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第21小时的总能耗
        /// </summary>
        public double Sum_H21 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第22小时的总能耗
        /// </summary>
        public double Sum_H22 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第23小时的总能耗
        /// </summary>
        public double Sum_H23 { set; get; }
        /// <summary>
        /// 指定类型能耗指定天的第24小时的总能耗
        /// </summary>
        public double Sum_H24 { set; get; }

        /// <summary>
        /// 指定类型能耗指定天的总能耗
        /// </summary>
        public double Sum_DayTotal { set; get; }

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
