using Shine.Core.Data;
using System;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class HostPolicy_0x5B_In : EntityBase<Guid>
    {
        /// <summary>
        /// 根据需求添加主键，该键无需值
        /// </summary>
        //public Guid Id { set; get; }

        /// <summary> 
        /// 远程主机的注册包 
        /// </summary> 
        public string RegPackage { set; get; }

        /// <summary> 
        /// 获取或设置 策略编号 
        /// </summary> 
        public int Number { set; get; }

        /// <summary> 
        /// 获取或设置 使能开关 
        /// 说明：0关，1开
        /// </summary> 
        public byte Enabled { set; get; }

        /// <summary> 
        /// 获取或设置 策略优先级 
        /// </summary> 
        public int Priority { set; get; }

        /// <summary> 
        /// 获取或设置 策略实行周期 
        /// </summary> 
        public int Cycle { set; get; }

        /// <summary> 
        /// 获取或设置 策略起始年 
        /// </summary> 
        public int StartYear { set; get; }

        /// <summary> 
        /// 获取或设置 策略起始月 
        /// </summary> 
        public int StartMonth { set; get; }

        /// <summary> 
        /// 获取或设置 策略起始日 
        /// </summary> 
        public int StartDay { set; get; }

        /// <summary> 
        /// 获取或设置 策略起始周 
        /// </summary> 
        public int StartWeek { set; get; }

        /// <summary> 
        /// 获取或设置 策略结束年 
        /// </summary> 
        public int EndYear { set; get; }

        /// <summary> 
        /// 获取或设置 策略结束月 
        /// </summary> 
        public int EndMonth { set; get; }

        /// <summary> 
        /// 获取或设置 策略结束日 
        /// </summary> 
        public int EndDay { set; get; }

        /// <summary> 
        /// 获取或设置 策略结束周 
        /// </summary> 
        public int EndWeek { set; get; }

        /// <summary> 
        /// 获取或设置 策略时间参考点 
        /// </summary> 
        public int Reference { set; get; }

        /// <summary> 
        /// 获取或设置 策略触发时 
        /// </summary> 
        public int Hour { set; get; }

        /// <summary> 
        /// 获取或设置 策略触发分 
        /// </summary> 
        public int Minute { set; get; }

        /// <summary> 
        /// 获取或设置 策略分组掩码 
        /// </summary> 
        public string GroupMask { set; get; }

        /// <summary> 
        /// 获取或设置 调光使能开关 
        /// 说明：0关，1开
        /// </summary> 
        public byte DimEnabled { set; get; }

        /// <summary> 
        /// 获取或设置 调光值 
        /// </summary> 
        public int DimValue { set; get; }

        /// <summary> 
        /// 获取或设置 回路开关 
        /// 说明：0关，1开
        /// </summary> 
        public byte Switch { set; get; }

        /// <summary> 
        /// 获取或设置 回路掩码 
        /// </summary> 
        public string LoopMsk { set; get; }
    }
}
