using Newtonsoft.Json;
using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Filter;
using Shine.Core.Caching;
using Shine.Core.Data.Extensions;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.Solib;
using Shine.WebApi.Models;
using Shine.WebApi.Models.OutDtos;
using Shine.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shine.WebApi.Controllers.API
{
    /// <summary>
    /// 远程升级相关接口
    /// </summary>
    [Description("远程升级相关接口")]
    public class UpgradeController : ThisBaseApiController
    {
        /// <summary>
        /// 升级包上传
        /// </summary>
        /// <param name="DataItemDetailId">升级包类型主键</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upgrade/AddUpgradeFile/{DataItemDetailId}")]
        [Description("远程升级-升级包上传")]
        public async Task<IHttpActionResult> AddUpgradeFile(Guid DataItemDetailId)
        {
            try
            {
                var cacheUser = GetCacheUser;//获取缓存判断
                if (!cacheUser.IsAdministrator)
                {
                    return Json(new OperationResult(OperationResultType.ValidError, "id:权限操作不足"));
                }
                else
                {
                    var file = await Request.SaveUploadFile(Guid.NewGuid().ToString().ToUpper(), SaveFileType.HostBrushBag);
                    UpgradePackages packages = new UpgradePackages();
                    bool _ok = false;
                    using (FileStream fs = new FileStream(file.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        byte[] bytes = new byte[fs.Length];
                        await fs.ReadAsync(bytes, 0, bytes.Length);
                        fs.Seek(0, SeekOrigin.Begin);
                        BinaryFormatter binary = new BinaryFormatter();
                        if (binary.Deserialize(fs) is PackInfo obb)
                        {
                            packages.CreatedTime = DateTime.Now;
                            packages.DataItemDetail_Id = DataItemDetailId;
                            packages.Datas = bytes;
                            packages.Id = obb.Key;
                            packages.PackName = obb.PackName;
                            packages.Version = obb.Version;
                            _ok = true;
                        }
                    }
                    file.Delete();
                    if (_ok)
                    {
                        return Json(UpgradeService.AddUpgradeFile(packages));
                    }
                    else
                    {
                        return Json(new OperationResult(OperationResultType.ValidError, "校正升级包错误"));
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new OperationResult(OperationResultType.Error, ex.Message));
            }
        }

        /// <summary>
        /// 删除指定升级包
        /// </summary>
        /// <param name="Id">指定要删除的升级包主键</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upgrade/DeleteUpgradeFile/{Id}")]
        [Description("远程升级-删除指定升级包")]

        public IHttpActionResult DeleteUpgrade(Guid Id) => Json(UpgradeService.TryCatchAction(
            action: m =>
            {
                var cacheUser = GetCacheUser;//获取缓存判断
                if (!cacheUser.IsAdministrator)
                {
                    throw new Exception("id:权限操作不足");
                }
                else
                {
                    return m.DeleteUpgradeFile(Id);
                }
            }));

        /// <summary>
        /// 获取升级包列表
        /// </summary>
        /// <param name="grid">数据刷选</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upgrade/GetUpgradeList")]
        [Description("远程升级-获取升级包列表")]
        public IHttpActionResult GetUpgradeList([FromBody]GridRequestsModel grid) => Json(UpgradeService.TryCatchAction(
            action: m =>
            {
                grid.CheckNotNull("grid");
                var cacheUser = GetCacheUser;
                //查询条件
                GridRequests request = new GridRequests(grid);
                //添加默认排序，只有排序未设置的情况下生效
                request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
                //获取查询集合
                Expression<Func<UpgradePackages, bool>> predicate = FilterHelper.GetExpression<UpgradePackages>(request.FilterGroup);
                var data = m.UpgradePackageQueryable.ToPage<UpgradePackages, UpgradePackagesOutDto>(predicate, request.PageCondition);
                return new OperationResult(OperationResultType.Success, "获取数据成功", data);
            }));

        /// <summary>
        /// 添加升级任务
        /// </summary>
        /// <param name="UpgradeId">升级包的主键</param>
        /// <param name="packs">准备升级的主机列表(注册包)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upgrade/AddUpgradeTasks/{UpgradeId}")]
        [Description("添加升级任务")]
        public async Task<IHttpActionResult> AddUpgradeTasks(Guid UpgradeId, [FromBody]params string[] packs) => Json(await UpgradeService.TryCatchAction(async m =>
       {

           UpgradeId.CheckNotEmpty("UpgradeId");
           packs.CheckNotNullOrEmpty("packs");
           var cacheUser = GetCacheUser;
           // 获取升级包
           var _upgradepack = m.UpgradePackageQueryable.FirstOrDefault(mt => mt.Id == UpgradeId);
           // 获取升级主机信息
           var _hosts = HostService.HostQueryable.Where(xm => packs.Contains(xm.RegPackage)).ToList();

           var _failNoIn = packs.Except(_hosts.Select(x => x.RegPackage).ToArray());

           List<UpgradeLog> upgrades = new List<UpgradeLog>();
           if (_upgradepack != null)
           {
               StringBuilder builder = new StringBuilder();
               for (int n = 0; n < _hosts.Count; n++)
               {
                   if (n == _hosts.Count - 1)
                   {
                       builder.Append(_hosts[n].RegPackage);
                   }
                   else
                   {
                       builder.Append($"{_hosts[n].RegPackage},");
                   }
               }

               using (HttpClient ht = new HttpClient())
               {
                   string baseUrl = string.Format("http://127.0.0.1:2500/Wcf/AddUpgradeTask?itype={0}", 1);
                   HttpContent httpContent = new ByteArrayContent(_upgradepack.Datas);
                   httpContent.Headers.Add("packes", builder.ToString());
                   var response = await ht.PostAsync(baseUrl, httpContent);
                   var result = await response.Content.ReadAsStringAsync();
                   Root dresult = JsonConvert.DeserializeObject<Root>(result);
                   List<UpgradeTask> tasks = dresult.NewIng;

                   List<UpgradeLog> upgradeLogs = new List<UpgradeLog>();
                   foreach (var ob in tasks)
                   {
                       upgradeLogs.Add(new UpgradeLog
                       {
                           Id = ob.Id,
                           CreatedTime = DateTime.Now,
                           CreatorUserId = cacheUser.UserName,
                           Organize_Id = _hosts.FirstOrDefault(xx => xx.RegPackage == ob.RegPack).Organize_Id,
                           CompleteTime = null,
                           Packet = $"{_upgradepack.PackName}&{_upgradepack.Version}",
                           State = 1, //计划中
                           Content = $"Host:[{ob.RegPack}]=>{_upgradepack.Version}" //这个地方不要改                           
                       });
                   }
                   dresult.Being.AddRange(_failNoIn);
                   return m.AddUpgradeLog(dresult.Being.ToArray(), upgradeLogs.ToArray());
               }
           }
           else
           {
               return new OperationResult(OperationResultType.ValidError, $"未找到升级包=={UpgradeId}==");
           }
       }));

        [HttpPost]
        [Route("Upgrade/EditUpgradeTasks")]
        [Description("更新升级记录状态")]
        public IHttpActionResult EditUpgradeTasks([FromBody]params EditUpgradeTask[] dynamics) => Json(UpgradeService.TryCatchAction(action: m =>
        {
            dynamics.CheckNotNullOrEmpty("dynamics");
            return UpgradeService.EditUpgradeLog(dynamics);
        }));

        [HttpPost]
        [Route("Upgrade/DeleteUpgradeTasks")]
        [Description("按主键删除指定的升级计划记录")]
        public IHttpActionResult DeleteUpgrateTask([FromBody] params Guid[] ids) => Json(UpgradeService.TryCatchAction(
            action: m =>
             {
                 ids.CheckNotNullOrEmpty("Ids");
                 var cacheUser = GetCacheUser;
                 return m.DeleteUpgradeLog(cacheUser, ListOrganizeId.ToArray(), ids);
             }));

        [HttpPost]
        [Route("Upgrade/GetUpgradeTaskList")]
        [Description("获取任务升级计划记录")]
        public IHttpActionResult GetUpgradeTaskList([FromBody]GridRequestsModel grid) => Json(UpgradeService.TryCatchAction(
         action: m =>
         {
             grid.CheckNotNull("grid");
             var cacheUser = GetCacheUser;
             //查询条件
             GridRequests request = new GridRequests(grid);
             //添加默认排序，只有排序未设置的情况下生效
             request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
             //获取查询集合
             Expression<Func<UpgradeLog, bool>> predicate = FilterHelper.GetExpression<UpgradeLog>(request.FilterGroup);
             var data = m.UpgradeLogQueryable.ToPage<UpgradeLog, UpgradeLogOutDto>(predicate, request.PageCondition);
             return new OperationResult(OperationResultType.Success, "获取数据成功", data);
         }));
    }


    #region 仅用于本类的实体类
    public class Root
    {
        /// <summary>
        /// 已开始的升级计划主机
        /// </summary>
        public List<string> Being { get; set; }

        /// <summary>
        /// 新添加的主机升级任务计划
        /// </summary>
        public List<UpgradeTask> NewIng { get; set; }
    }

    public class UpgradeTask
    {
        public UpgradeTask(Guid id, string regPack)
        {
            Id = id;
            RegPack = regPack;
        }

        /// <summary>
        /// 任务主键
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 主机注册包
        /// </summary>
        public string RegPack { set; get; }
    }

    public class EditUpgradeTask
    {
        public Guid Id { set; get; }
        public int State { set; get; }
    }
    #endregion
}
