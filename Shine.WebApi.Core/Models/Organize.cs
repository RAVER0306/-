using Shine.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.WebApi.Core.Models
{
    //public class Organize : EntityBase<Int32>, ICreatedAudited, IUpdateAudited,ILockable
    //{
    //    #region Models
    //    /// <summary> 
    //    /// 该组织的父级Id 
    //    /// </summary> 
    //    public Int32? ParentId { set; get; }

    //    /// <summary> 
    //    /// 组织联系电话 
    //    /// </summary> 
    //    [StringLength(64)]
    //    public string TelePhone { set; get; }

    //    /// <summary> 
    //    /// 组织传真 
    //    /// </summary> 
    //    [StringLength(64)]
    //    public string Fax { set; get; }

    //    /// <summary> 
    //    /// 该组织的编码 
    //    /// </summary> 
    //    [StringLength(64)]
    //    public string EnCode { set; get; }

    //    /// <summary> 
    //    /// 该组织的全称 
    //    /// </summary> 
    //    [StringLength(124)]
    //    public string FullName { set; get; }

    //    /// <summary> 
    //    /// 该组织的简称 
    //    /// </summary> 
    //    [StringLength(64)]
    //    public string ShortName { set; get; }

    //    /// <summary> 
    //    /// 组织邮箱 
    //    /// </summary> 
    //    [StringLength(64)]
    //    public string Email { set; get; }

    //    /// <summary> 
    //    /// 组织属于的国家 
    //    /// </summary> 
    //    [StringLength(1000)]
    //    public string Country { set; get; }

    //    /// <summary> 
    //    /// 组织属于的城市 
    //    /// </summary> 
    //    [StringLength(1000)]
    //    public string City { set; get; }

    //    /// <summary> 
    //    /// 组织的属于的省 
    //    /// </summary> 
    //    [StringLength(1000)]
    //    public string Province { set; get; }

    //    /// <summary> 
    //    /// 组织属于的县/区 
    //    /// </summary> 
    //    [StringLength(1000)]
    //    public string County { set; get; }

    //    /// <summary> 
    //    /// 该组织的详细地址 
    //    /// </summary> 
    //    [StringLength(512)]
    //    public string Address { set; get; }

    //    /// <summary> 
    //    /// 描述 
    //    /// </summary> 
    //    [StringLength(512)]
    //    public string Description { set; get; }

    //    /// <summary> 
    //    /// 排序码 
    //    /// </summary> 
    //    public int? SortCode { set; get; }

    //    /// <summary> 
    //    /// 该组织的属于树形结构中的层次 
    //    /// </summary> 
    //    [Range(0,8)]
    //    public byte? Layers { set; get; }

    //    /// <summary> 
    //    /// 组织类型：0:机构 1:项目 
    //    /// </summary> 
    //    [Range(0,1)]
    //    public byte? Type { set; get; }

    //    /// <summary>
    //    /// 创建该组织信息的时间
    //    /// </summary>
    //    public DateTime CreatedTime { set; get; }

    //    /// <summary>
    //    ///  创建该用户信息的组织ID
    //    /// </summary>
    //    [StringLength(64)]
    //    public string CreatorUserId { set; get; }

    //    /// <summary>
    //    /// 最后更新该组织信息时间
    //    /// </summary>
    //    public DateTime? LastUpdatedTime { set; get; }

    //    /// <summary>
    //    /// 最后更新该组织信息的用户ID
    //    /// </summary>
    //    [StringLength(64)]
    //    public string LastUpdatorUserId { set; get; }

    //    /// <summary>
    //    /// 机构是否被锁定
    //    /// </summary>
    //    public bool IsLocked { get; set; }      
    //    #endregion
    //}
}
