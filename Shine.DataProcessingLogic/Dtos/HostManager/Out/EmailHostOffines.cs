using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.Out
{
    public class EmailHostOffines
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrgName { set; get; }

        /// <summary>
        /// 主机注册包
        /// </summary>
        public string RegPackage { set; get; }

        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { set; get; }

        /// <summary>
        /// 主机三相电压
        /// </summary>
        public string Voltage { set; get; }

        /// <summary>
        /// 三相电电流
        /// </summary>
        public string Current { set; get; }

        /// <summary>
        /// 三项点功率
        /// </summary>
        public string Power { set; get; }

        /// <summary>
        /// 主机温度
        /// </summary>
        public int Temperature { set; get; }

        /// <summary>
        /// 主机数据最后更新时间
        /// </summary>
        public DateTime UpdateTime { set; get; }

        /// <summary>
        /// 帐号层次
        /// </summary>
        public byte Level { set; get; }
    }
}
