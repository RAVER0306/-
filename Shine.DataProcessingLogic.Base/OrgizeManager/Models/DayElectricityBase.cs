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
    /// 天电量统计实体基类
    /// </summary>
    /// <typeparam name="TKey">天电量统计实体主键类型</typeparam>
    public abstract class DayElectricityBase<TKey> :
        EntityBase<TKey>,
        ICreatedTime
        where TKey : IEquatable<TKey>
    {
        private double _h1 = 0;
        private double _h2 = 0;
        private double _h3 = 0;
        private double _h4 = 0;
        private double _h5 = 0;
        private double _h6 = 0;
        private double _h7 = 0;
        private double _h8 = 0;
        private double _h9 = 0;
        private double _h10 = 0;
        private double _h11 = 0;
        private double _h12 = 0;
        private double _h13 = 0;
        private double _h14 = 0;
        private double _h15 = 0;
        private double _h16 = 0;
        private double _h17 = 0;
        private double _h18 = 0;
        private double _h19 = 0;
        private double _h20 = 0;
        private double _h21 = 0;
        private double _h22 = 0;
        private double _h23 = 0;
        private double _h24 = 0;
        
        /// <summary>
        /// 根据当前小时获取或设置能耗值
        /// </summary>
        /// <param name="index">当前小时</param>
        /// <returns></returns>
        public double this[int index]
        {
            set
            {
                switch (index)
                {
                    case 0: _h24 = value; break;
                    case 1: _h1 = value; break;
                    case 2: _h2 = value; break;
                    case 3: _h3 = value; break;
                    case 4: _h4 = value; break;
                    case 5: _h5 = value; break;
                    case 6: _h6 = value; break;
                    case 7: _h7 = value; break;
                    case 8: _h8 = value; break;
                    case 9: _h9 = value; break;
                    case 10: _h10 = value; break;
                    case 11: _h11 = value; break;
                    case 12: _h12 = value; break;
                    case 13: _h13 = value; break;
                    case 14: _h14 = value; break;
                    case 15: _h15 = value; break;
                    case 16: _h16 = value; break;
                    case 17: _h17 = value; break;
                    case 18: _h18 = value; break;
                    case 19: _h19 = value; break;
                    case 20: _h20 = value; break;
                    case 21: _h21 = value; break;
                    case 22: _h22 = value; break;
                    case 23: _h23 = value; break;
                    default: throw new Exception("索引超出范围！");
                }
            }
            get
            {
                switch (index)
                {
                    case 0:return _h24;
                    case 1: return _h1;
                    case 2: return _h2;
                    case 3: return _h3;
                    case 4: return _h4;
                    case 5: return _h5;
                    case 6: return _h6;
                    case 7: return _h7;
                    case 8: return _h8;
                    case 9: return _h9;
                    case 10: return _h10;
                    case 11: return _h11;
                    case 12: return _h12;
                    case 13: return _h13;
                    case 14: return _h14;
                    case 15: return _h15;
                    case 16: return _h16;
                    case 17: return _h17;
                    case 18: return _h18;
                    case 19: return _h19;
                    case 20: return _h20;
                    case 21: return _h21;
                    case 22: return _h22;
                    case 23: return _h23;
                    default: throw new Exception("索引超出范围！");
                }
            }
        }

        /// <summary> 
        /// 获取或设置 当前数 
        /// </summary> 
        public int Today { set; get; }

        /// <summary> 
        /// 获取或设置 最后更新时间 
        /// </summary> 
        public DateTime UpdateTime { set; get; }

        /// <summary> 
        /// 获取或设置 一天的总能耗 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double DayTotal { set; get; }

        /// <summary> 
        /// 获取或设置 一天中的第1小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H1 { get => _h1; set => _h1 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第2小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H2 { get => _h2; set => _h2 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第3小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H3 { get => _h3; set => _h3 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第4小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H4 { get => _h4; set => _h4 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第5小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H5 { get => _h5; set => _h5 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第6小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H6 { get => _h6; set => _h6 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第7小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H7 { get => _h7; set => _h7 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第8小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H8 { get => _h8; set => _h8 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第9小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H9 { get => _h9; set => _h9 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第10小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H10 { get => _h10; set => _h10 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第11小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H11 { get => _h11; set => _h11 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第12小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H12 { get => _h12; set => _h12 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第13小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H13 { get => _h13; set => _h13 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第14小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H14 { get => _h14; set => _h14 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第15小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H15 { get => _h15; set => _h15 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第16小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H16 { get => _h16; set => _h16 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第17小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H17 { get => _h17; set => _h17 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第18小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H18 { get => _h18; set => _h18 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第19小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H19 { get => _h19; set => _h19 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第20小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H20 { get => _h20; set => _h20 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第21小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H21 { get => _h21; set => _h21 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第22小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H22 { get => _h22; set => _h22 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第23小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H23 { get => _h23; set => _h23 = value; }

        /// <summary> 
        /// 获取或设置 一天中的第24小时 
        /// </summary> 
        [Range(0, Int32.MaxValue)]
        public double H24 { get => _h24; set => _h24 = value; }


        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
