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
    /// 月度电量统计实体基类
    /// </summary>
    /// <typeparam name="TKey">月度电量统计实体主键类型</typeparam>
    public abstract class MonthElectricityBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        private double _d1 = 0;
        private double _d2 = 0;
        private double _d3 = 0;
        private double _d4 = 0;
        private double _d5 = 0;
        private double _d6 = 0;
        private double _d7 = 0;
        private double _d8 = 0;
        private double _d9 = 0;
        private double _d10 = 0;
        private double _d11 = 0;
        private double _d12 = 0;
        private double _d13 = 0;
        private double _d14 = 0;
        private double _d15 = 0;
        private double _d16 = 0;
        private double _d17 = 0;
        private double _d18 = 0;
        private double _d19 = 0;
        private double _d20 = 0;
        private double _d21 = 0;
        private double _d22 = 0;
        private double _d23 = 0;
        private double _d24 = 0;
        private double _d25 = 0;
        private double _d26 = 0;
        private double _d27 = 0;
        private double _d28 = 0;
        private double _d29 = 0;
        private double _d30 = 0;
        private double _d31 = 0;
        private int _tag = 0;
        private double _lastTagValue = 0;

        /// <summary>
        /// 根据当前天获取或设置指定索引的值
        /// </summary>
        /// <param name="index">当前月的第几天</param>
        /// <returns></returns>
        public double this[int index]
        {
            set
            {
                switch (index)
                {
                    case 1: _d1 = value; break;
                    case 2: _d2 = value; break;
                    case 3: _d3 = value; break;
                    case 4: _d4 = value; break;
                    case 5: _d5 = value; break;
                    case 6: _d6 = value; break;
                    case 7: _d7 = value; break;
                    case 8: _d8 = value; break;
                    case 9: _d9 = value; break;
                    case 10: _d10 = value; break;
                    case 11: _d11 = value; break;
                    case 12: _d12 = value; break;
                    case 13: _d13 = value; break;
                    case 14: _d14 = value; break;
                    case 15: _d15 = value; break;
                    case 16: _d16 = value; break;
                    case 17: _d17 = value; break;
                    case 18: _d18 = value; break;
                    case 19: _d19 = value; break;
                    case 20: _d20 = value; break;
                    case 21: _d21 = value; break;
                    case 22: _d22 = value; break;
                    case 23: _d23 = value; break;
                    case 24: _d24 = value; break;
                    case 25: _d25 = value; break;
                    case 26: _d26 = value; break;
                    case 27: _d27 = value; break;
                    case 28: _d28 = value; break;
                    case 29: _d29 = value; break;
                    case 30: _d30 = value; break;
                    case 31: _d31 = value; break;
                    default: throw new Exception("索引超出范围！");
                }
            }
            get
            {
                switch (index)
                {
                    case 1: return _d1;
                    case 2: return _d2;
                    case 3: return _d3;
                    case 4: return _d4;
                    case 5: return _d5;
                    case 6: return _d6;
                    case 7: return _d7;
                    case 8: return _d8;
                    case 9: return _d9;
                    case 10: return _d10;
                    case 11: return _d11;
                    case 12: return _d12;
                    case 13: return _d13;
                    case 14: return _d14;
                    case 15: return _d15;
                    case 16: return _d16;
                    case 17: return _d17;
                    case 18: return _d18;
                    case 19: return _d19;
                    case 20: return _d20;
                    case 21: return _d21;
                    case 22: return _d22;
                    case 23: return _d23;
                    case 24: return _d24;
                    case 25: return _d25;
                    case 26: return _d26;
                    case 27: return _d27;
                    case 28: return _d28;
                    case 29: return _d29;
                    case 30: return _d30;
                    case 31: return _d31;
                    default: throw new Exception("索引超出范围！");
                }
            }
        }

        /// <summary> 
        /// 获取或设置 当前月份
        /// </summary> 
        public int Month { set; get; }

        /// <summary> 
        /// 获取或设置 最后更新时间 
        /// </summary> 
        public DateTime UpdateTime { set; get; }

        /// <summary> 
        /// 获取或设置 月能耗总计 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double MonthTotal { set; get; }

        /// <summary> 
        /// 获取或设置 一个月中的第1天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D1 { get => _d1; set => _d1 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第2天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D2 { get => _d2; set => _d2 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第3天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D3 { get => _d3; set => _d3 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第4天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D4 { get => _d4; set => _d4 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第5天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D5 { get => _d5; set => _d5 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第6天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D6 { get => _d6; set => _d6 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第7天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D7 { get => _d7; set => _d7 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第8天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D8 { get => _d8; set => _d8 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第9天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D9 { get => _d9; set => _d9 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第10天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D10 { get => _d10; set => _d10 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第11天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D11 { get => _d11; set => _d11 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第12天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D12 { get => _d12; set => _d12 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第13天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D13 { get => _d13; set => _d13 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第14天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D14 { get => _d14; set => _d14 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第15天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D15 { get => _d15; set => _d15 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第16天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D16 { get => _d16; set => _d16 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第17天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D17 { get => _d17; set => _d17 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第18天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D18 { get => _d18; set => _d18 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第19天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D19 { get => _d19; set => _d19 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第20天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D20 { get => _d20; set => _d20 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第21天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D21 { get => _d21; set => _d21 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第22天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D22 { get => _d22; set => _d22 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第23天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D23 { get => _d23; set => _d23 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第24天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D24 { get => _d24; set => _d24 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第25天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D25 { get => _d25; set => _d25 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第26天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D26 { get => _d26; set => _d26 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第27天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D27 { get => _d27; set => _d27 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第28天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D28 { get => _d28; set => _d28 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第29天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D29 { get => _d29; set => _d29 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第30天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D30 { get => _d30; set => _d30 = value; }

        /// <summary> 
        /// 获取或设置 一个月中的第31天 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double D31 { get => _d31; set => _d31 = value; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置当前月主机能耗如果存在更换或能耗清空操作标志位
        /// 如果为0，则未进行过操作
        /// </summary>
        public int Tag { get => _tag; set => _tag = value; }

        /// <summary>
        /// 获取或设置当前月主机能耗如果存在更换或能耗清空操作时最后记录的值
        /// </summary>
        [Range(0, Int32.MaxValue)]
        public double LastTagValue { get => _lastTagValue; set => _lastTagValue = value; }
    }
}
