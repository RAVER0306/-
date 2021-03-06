﻿using Shine.Core.Data;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.Out;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shine.WebApi.Models
{
    public class OrganizePageOutDto : IOutputDto
    {
        /// <summary>
        /// 组织机构主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 获取或设置 该组织的父级组织主键
        /// </summary>
        public Guid? ParentId { set; get; }

        /// <summary>
        /// 获取或设置 该组织的联系电话
        /// </summary>
        [StringLength(32)]
        public string TelePhone { set; get; }

        /// <summary>
        /// 获取或设置 该组织的传真号码
        /// </summary>
        [StringLength(64)]
        public string Fax { set; get; }

        /// <summary>
        /// 获取或设置 该组织的名称
        /// </summary>
        [StringLength(128)]
        public string FullName { set; get; }

        /// <summary>
        /// 获取或设置 该组织的电子邮箱
        /// </summary>
        [StringLength(64), EmailAddress]
        public string Email { set; get; }

        /// <summary>
        /// 获取或设置 该组织所属的国家
        /// </summary>
        [StringLength(64)]
        public string Country { set; get; }

        /// <summary>
        /// 获取或设置 该组织所属的省
        /// </summary>
        [StringLength(64)]
        public string Province { set; get; }

        /// <summary>
        /// 获取或设置 该组织所属的城市
        /// </summary>
        [StringLength(64)]
        public string City { set; get; }

        /// <summary>
        /// 获取或设置 该组织所属的区或县
        /// </summary>
        [StringLength(64)]
        public string County { set; get; }

        /// <summary>
        /// 获取或设置 该组织的详细地址
        /// </summary>
        [StringLength(512)]
        public string Address { set; get; }

        /// <summary>
        /// 获取或设置 该组织的信息说明
        /// </summary>
        [StringLength(512)]
        public string Remark { set; get; }

        /// <summary>
        /// 获取或设置 该组织的排序码
        /// </summary>        
        public int SortCode { set; get; }

        /// <summary>
        /// 获取或设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 创建该信息的用户ID
        /// </summary>
        [StringLength(128)]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 获取或设置 信息最后被修改的时间
        /// </summary>
        public DateTime? LastUpdatedTime { set; get; }

        /// <summary>
        /// 获取或设置 修改该信息的用户ID
        /// </summary>
        [StringLength(128)]
        public string LastUpdatorUserId { set; get; }

        /// <summary>
        /// 获取或设置 组织机构的关联 <see cref="DataItemDetail"/> 类型
        /// </summary>
        public DataItemDetailOutDto DataItemDetailOne { set; get; }
    }
}