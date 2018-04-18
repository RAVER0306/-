/*===================================================
* 类名称: PackInfo
* 类描述:
* 创建人: 72440
* 创建时间: 2018/3/22 13:43:12
* 修改人: 
* 修改时间:
* 修改原因:
* 版本：version 1.0
=====================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.Solib
{
    [Serializable]
    public class PackInfo
    {
        /// <summary>
        /// 升级包类型枚举
        /// </summary>
        public enum EUpgradePackType
        {
            standard,
            NonStandard
        }

        /// <summary>
        /// 包名称
        /// </summary>

        public string PackName { set; get; }

        /// <summary>
        /// 包版本
        /// </summary>
        public string Version { set; get; }

        /// <summary>
        /// 升级包类型
        /// </summary>
        public EUpgradePackType UpgradePackType { set; get; }

        /// <summary>
        /// 升级包数据
        /// </summary>
        public byte[] Datas { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 文件标识
        /// </summary>
        public Guid Key { set; get; }
    }
}
