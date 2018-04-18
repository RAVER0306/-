using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Dtos.HostManager.In
{
    public class SubControlInputDto : IInputDto<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 分控编号
        /// </summary>
        public int SubNum { set; get; }

        /// <summary>
        /// 获取或设置 分控名称
        /// </summary>
        [StringLength(64)]
        public string SubName { set; get; }

        /// <summary>
        /// 获取或设置 分控UID
        /// </summary>
        [StringLength(64)]
        public string UID { set; get; }

        /// <summary>
        /// 获取或设置 分控信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 分控类型的主键
        /// </summary>
        public Guid DataItemDetail_Id { set; get; }

        /// <summary>
        /// 获取或设置 <see cref="LightPole"/>关联主键
        /// </summary>
        public Guid LigthPoleOne_Id { set; get; }

        /// <summary> 
        ///  获取或设置 分控位置经度
        /// </summary> 
        public double Longitude { set; get; }

        /// <summary> 
        ///  获取或设置 分控位置纬度
        /// </summary> 
        public double Latitude { set; get; }
    }
}
