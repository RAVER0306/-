using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.Out
{
    public class EmailSubOffines
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
        /// 灯杆编号
        /// </summary>
        public int PoleNum { set; get; }

        /// <summary>
        /// 灯杆名称
        /// </summary>
        public string PoleName { set; get; }

        /// <summary>
        /// 分控编号
        /// </summary>
        public int SubNum { set; get; }

        /// <summary>
        /// 分控名称
        /// </summary>
        public string SubName { set; get; }

        /// <summary>
        /// 灯具端口
        /// </summary>
        public int DimmingPort { set; get; }

        /// <summary>
        /// 分控数据最后更新时间
        /// </summary>
        public DateTime UpdateTime { set; get; }

        /// <summary>
        /// 帐号层次
        /// </summary>
        public byte Level { set; get; }
    }
}
