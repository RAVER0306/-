using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Filter;
using Shine.Comman.Secutiry;
using Shine.Core.Data.Extensions;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.WebApi.Models;
using Shine.WebApi.Utils;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace Shine.WebApi.Controllers.API
{

    /// <summary>
    /// 数据字典相关信息管理接口
    /// </summary>
    [Description("据字典相关信息管理接口")]
    public class DataItemController : ThisBaseApiController
    {
        #region 获取数据字典目录
        /// <summary>
        /// 获取数据字典目录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取数据字典目录")]
        [Route("DataItem/GetDataItems")]
        public IHttpActionResult GetDataItems([FromBody]GridRequestsModel grid) => Json(DataItemService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;//获取缓存判断

                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception("id:你没有权限进行此项操作！");
                }

                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));

                // 查询所有公开的数据字典目录
                var queryable1 = m.DataItemQueryable.Where(a => a.IsPublic == true);
                // 系统管理员可以查询全部
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = m.DataItemQueryable;
                }          

                Expression<Func<DataItem, bool>> predicate = FilterHelper.GetExpression<DataItem>(request.FilterGroup);

                var data = queryable1.ToPage<DataItem, DataItemPageOutDto>(predicate, request.PageCondition);               
                return new OperationResult(OperationResultType.Success, "操作成功", data);
            }));
        #endregion

        #region 添加字典数据目录(系统管理员能添加)
        [HttpPost]
        [Route("DataItem/AddDataItems")]
        [Description("添加字典数据目录")]
        public IHttpActionResult AddDataItems([FromBody]params DataItemInputDto[] datas) => Json(DataItemService.TryCatchAction(
           action: m =>
           {
               datas.CheckNotNullOrEmpty("datas");
               if (!GetCacheUser.IsAdministrator)
               {
                   throw new Exception($"id:你的权限不足，只有系统管理员才能进行此项操作！");
               }
               return m.AddDataItems(datas);
           }));
        #endregion

        #region 删除字典数据目录(系统管理员能删除)
        [HttpPost]
        [Route("DataItem/DeleteDataItems")]
        [Description("删除字典数据目录")]
        public IHttpActionResult DeleteDataItems([FromBody]params Guid[] Ids) => Json(DataItemService.TryCatchAction(
            action: m =>
             {
                 Ids.CheckNotNullOrEmpty("Ids");
                 if (!GetCacheUser.IsAdministrator)
                 {
                     throw new Exception($"id:你的权限不足，只有系统管理员才能进行此项操作！");
                 }
                 return m.DeleteDataItems(Ids);
             }));
        #endregion

        #region 编辑字典数据目录
        [HttpPost]
        [Route("DataItem/EditDataItems")]
        [Description("编辑字典数据目录")]
        public IHttpActionResult EditDataItems([FromBody]params DataItemInputDto[] datas) => Json(DataItemService.TryCatchAction(
            action: m =>
             {
                 datas.CheckNotNullOrEmpty("datas");
                 if (!GetCacheUser.IsAdministrator)
                 {
                     throw new Exception($"id:你的权限不足，只有系统管理员才能进行此项操作！");
                 }
                 return m.EditDataItems(datas);
             }));
        #endregion

        #region 根据字典Id获取字典内容
        [HttpGet]
        [Route("DataItem/GetDataItemDetailById/{id}")]
        [Description(" 根据字典Id获取字典内容")]
        public IHttpActionResult GetDataItemDetailById(Guid Id) => Json(DataItemService.TryCatchAction(
            action:m=> 
            {
                var cacheUser = GetCacheUser;

                var queryable1 = from a in m.DataItemDetailQueryable
                                 join b in m.DataItemQueryable
                                 on a.DataItem_Id equals b.Id
                                 where a.DataItem_Id == Id && (a.Organzie_Id == null || ListOrganizeId.Contains(a.Organzie_Id.Value)) && b.IsPublic == true
                                 select new
                                 {
                                     Id = a.Id,
                                     FullName = a.FullName,
                                     QueryCoding = a.QueryCoding,
                                     IsLocked = a.IsLocked,
                                     IsPublic = a.IsPublic,
                                     Remark = a.Remark,
                                     CreatedTime = a.CreatedTime,
                                     Index = a.Index,
                                     IsSystem = a.IsSystem,
                                     OrganizeName = a.OrganizeOne.FullName
                                 };
                if (cacheUser.IsAdministrator)
                {
                    queryable1 = m.DataItemDetailQueryable.Where(b => b.DataItem_Id == Id)
                    .Select(a => new
                    {
                        Id = a.Id,
                        FullName = a.FullName,
                        QueryCoding = a.QueryCoding,
                        IsLocked = a.IsLocked,
                        IsPublic = a.IsPublic,
                        Remark = a.Remark,
                        CreatedTime = a.CreatedTime,
                        Index = a.Index,
                        IsSystem = a.IsSystem,
                        OrganizeName = a.OrganizeOne.FullName
                    });
                }

                var result = queryable1.ToArray();
                return new OperationResult(OperationResultType.Success, "获取结果成功", result);
            }));
        #endregion

        #region 修改字典目录数据的详细信息
        [HttpPost]
        [Route("DataItem/EditDataItemDetails")]
        [Description("修改字典目录数据的详细信息")]
        public IHttpActionResult EditDataItemDetails([FromBody] params DataItemDetailInputDto[] datas) => Json(DataItemService.TryCatchAction(
            action: m =>
             {
                 datas.CheckNotNullOrEmpty("datas");
                 var cacheUser = GetCacheUser;
                 if (!cacheUser.Level.IsBetween(1, 2))
                 {
                     throw new Exception($"id:你的权限不足，不能进行此项操作！");
                 }
                 return m.EditDataItemDetails(cacheUser, datas);
             }));
        #endregion

        #region 删除字典目录数据的详细信息
        [HttpPost]
        [Route("DataItem/DeleteDataItemDetails")]
        [Description("删除字典目录数据的详细信息")]
        public IHttpActionResult DeleteDataItemDetails(params Guid[] Ids) => Json(DataItemService.TryCatchAction(
            action: m =>
            {
                Ids.CheckNotNullOrEmpty("Ids");
                var cacheUser = GetCacheUser;
                return m.DeleteDataItemDetails(cacheUser, Ids);
            }));
        #endregion

        #region 添加字典目录数据的详细信息
        [HttpPost]
        [Route("DataItem/AddDataItemDetails")]
        [Description("添加字典目录数据的详细信息")]
        public IHttpActionResult AddDataItemDetails([FromBody] params DataItemDetailInputDto[] datas) => Json(DataItemService.TryCatchAction(
            action: m =>
            {
                datas.CheckNotNullOrEmpty("datas");
                var cacheUser = GetCacheUser;
                if (!cacheUser.Level.IsBetween(1, 2))
                {
                    throw new Exception($"id:你的权限不足，不能进行此项操作！");
                }
                return m.AddDataItemDetails(cacheUser, datas);
            }));
        #endregion

        #region 根据查询码查询字典目录的详细信息
        [HttpGet]
        [Route("DataItem/GetAppointType/{EnQuerying}")]
        [Description("根据查询码查询字典目录的详细信息")]
        public IHttpActionResult GetAppointType(string EnQuerying) => Json(DataItemService.TryCatchAction(
            action: m =>
             {
                 EnQuerying.CheckNotNullOrEmpty("EnQuerying");

                 string unQuerying = EnQuerying.AESDecrypt();

                 var cacheUser = GetCacheUser;

                 var queryable1 = from a in m.DataItemDetailQueryable
                                  where a.QueryCoding == unQuerying &&
                                  (a.Organzie_Id == null || ListOrganizeId.Contains(a.Organzie_Id.Value))
                                  && a.IsLocked == false
                                  orderby a.Index ascending
                                  select new { Id = a.Id, FullName = a.FullName, Index = a.Index };

                 if (cacheUser.IsAdministrator)
                 {
                     queryable1 = m.DataItemDetailQueryable.Where(b => b.QueryCoding == unQuerying && b.IsPublic == true&&b.IsLocked==false)
                                  .Select(a => new { Id = a.Id, FullName = a.FullName, Index = a.Index });
                 }
                 var result = queryable1.ToArray();
                 return new OperationResult( OperationResultType.Success,"获取结果成功",result);
             }));
        #endregion
    }
}
