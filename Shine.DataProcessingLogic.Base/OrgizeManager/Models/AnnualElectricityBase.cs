using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Base.OrgizeManager.Models
{
    /// <summary>
    /// 年度电量统计实体基类
    /// </summary>
    /// <typeparam name="TKey">年度电量统计实体主键类型</typeparam>
    public abstract class AnnualElectricityBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        private double _m1 = 0;
        private double _m2 = 0;
        private double _m3 = 0;
        private double _m4 = 0;
        private double _m5 = 0;
        private double _m6 = 0;
        private double _m7 = 0;
        private double _m8 = 0;
        private double _m9 = 0;
        private double _m10 = 0;
        private double _m11 = 0;
        private double _m12 = 0;
        private double _yearTotal = 0;
        private double _tagCumulative = -1;

        /// <summary>
        /// 获取或设置当前月的能耗值
        /// </summary>
        /// <param name="index">当前月</param>
        /// <returns></returns>
        public double this[int index]
        {
            set
            {
                switch (index)
                {
                    case 1: _m1 = value; break;
                    case 2: _m2 = value; break;
                    case 3: _m3 = value; break;
                    case 4: _m4 = value; break;
                    case 5: _m5 = value; break;
                    case 6: _m6 = value; break;
                    case 7: _m7 = value; break;
                    case 8: _m8 = value; break;
                    case 9: _m9 = value; break;
                    case 10: _m10 = value; break;
                    case 11: _m11 = value; break;
                    case 12: _m12 = value; break;
                    default: throw new Exception("索引超出范围！");
                }
            }
            get
            {
                switch (index)
                {
                    case 1: return _m1;
                    case 2: return _m2;
                    case 3: return _m3;
                    case 4: return _m4;
                    case 5: return _m5;
                    case 6: return _m6;
                    case 7: return _m7;
                    case 8: return _m8;
                    case 9: return _m9;
                    case 10: return _m10;
                    case 11: return _m11;
                    case 12: return _m12;
                    default: throw new Exception("索引超出范围！");
                }
            }
        }


        /// <summary> 
        /// 获取或设置 1月总能耗 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double M1 { get => _m1; set => _m1 = value; }

        /// <summary> 
        /// 获取或设置 2月总能耗 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double M2 { get => _m2; set => _m2 = value; }

        /// <summary> 
        /// 获取或设置 3月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M3 { get => _m3; set => _m3 = value; }

        /// <summary> 
        /// 获取或设置 4月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M4 { get => _m4; set => _m4 = value; }

        /// <summary> 
        /// 获取或设置 5月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M5 { get => _m5; set => _m5 = value; }

        /// <summary> 
        /// 获取或设置 6月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M6 { get => _m6; set => _m6 = value; }

        /// <summary> 
        /// 获取或设置 7月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M7 { get => _m7; set => _m7 = value; }

        /// <summary> 
        /// 获取或设置 8月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M8 { get => _m8; set => _m8 = value; }

        /// <summary> 
        /// 获取或设置 9月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M9 { get => _m9; set => _m9 = value; }

        /// <summary> 
        /// 获取或设置 10月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M10 { get => _m10; set => _m10 = value; }

        /// <summary> 
        /// 获取或设置 11月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M11 { get => _m11; set => _m11 = value; }

        /// <summary> 
        /// 获取或设置 12月总能耗 
        /// </summary> 
         [Range(0, Int32.MaxValue)]
        public double M12 { get => _m12; set => _m12 = value; }

        /// <summary> 
        /// 获取或设置 最后更新时间 
        /// </summary> 
        public DateTime UpdateTime { set; get; }

        /// <summary> 
        /// 获取或设置 当前年 
        /// </summary> 
        public int Year { set; get; }

        /// <summary> 
        /// 获取或设置 当前年的总能耗 
        /// </summary> 

        [Range(0, Int32.MaxValue)]
        public double YearTotal { get => _yearTotal; set => _yearTotal = value; }


        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 历史累计能耗值
        /// </summary>
        [Range(0, Int32.MaxValue)]
        public double Cumulative { set; get; }

        /// <summary>
        /// 获取或设置 那些能耗信息一直累计的设备在更换后新一轮的累计值
        /// -1表这个设备未有被更换过，此字段仅仅用于累计能耗设备的标记
        /// </summary>
        [Range(-1, Int32.MaxValue)]
        public double TagCumulative { get => _tagCumulative; set => _tagCumulative = value; }
    }
}
