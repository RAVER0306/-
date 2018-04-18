using Newtonsoft.Json;
using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Data;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.Services
{
    public class UpgradeService : IUpgradeContract
    {
        /// <summary>
        /// 表<see cref="UpgradePackages"/>数据仓储
        /// </summary>
        public IRepository<UpgradePackages, Guid> UpgradeRepository{ set; get; }
        public IRepository<UpgradeLog, Guid> UpgradeLogRepository { set; get; }
        public IQueryable<UpgradePackages> UpgradePackageQueryable =>UpgradeRepository.Entities;
        public IQueryable<UpgradeLog> UpgradeLogQueryable => UpgradeLogRepository.Entities;

        /// <summary>
        /// 添加升级包信息
        /// </summary>
        /// <param name="packages">主机升级包信息实体类</param>
        /// <returns></returns>
        public OperationResult AddUpgradeFile(UpgradePackages packages)
        {
            if (UpgradeRepository.CheckExists(m => m.Id == packages.Id))
            {
                return new OperationResult(OperationResultType.NoChanged, "该升级包已经存在");
            }
            else
            {
                return UpgradeRepository.Insert(packages) > 0 ?
                    new OperationResult(OperationResultType.Success, "添加升级包成功") :
                    new OperationResult(OperationResultType.NoChanged, "添加升级包失败");
            }
        }

        /// <summary>
        /// 删除指定升级包
        /// </summary>
        /// <param name="Id">升级包主键标识</param>
        /// <returns></returns>
        public OperationResult DeleteUpgradeFile(Guid Id)
        {
            return UpgradeRepository.DeleteDirect(Id) > 0 ?
                   new OperationResult(OperationResultType.Success, "删除指定升级包成功") :
                   new OperationResult(OperationResultType.NoChanged, "删除指定升级包失败");
        }

        /// <summary>
        /// 添加升级任务计划记录
        /// </summary>
        /// <param name="guids">已存在的升级计划主机</param>
        /// <param name="upgrades">需要添加的升级计划记录的主机集合</param>
        /// <returns></returns>
        public OperationResult AddUpgradeLog(string[] guids, params UpgradeLog[] upgrades)
        {
            try
            {
                if (upgrades.Length < 0)
                {
                    return new OperationResult(OperationResultType.NoChanged, $"当前不存在升级对象获取升级对象已经存在任务计划中", guids);
                }
                else
                {
                    List<string> _okList = new List<string>();
                    UpgradeLogRepository.UnitOfWork.BeginTransaction();
                    for (int n = 0; n < upgrades.Length; n++)
                    {
                        if (UpgradeLogRepository.Insert(upgrades[n]) > 0)
                        {
                            int start = upgrades[n].Content.IndexOf('[') + 1;
                            int end = upgrades[n].Content.IndexOf(']');
                            _okList.Add(($"[{upgrades[n].Content.Substring(start, end - start)}]"));
                        }
                    }
                    UpgradeLogRepository.UnitOfWork.Commit();
                    return new OperationResult(OperationResultType.Success, $"{JsonConvert.SerializeObject(_okList)}=>成功添加到任务升级计划,{JsonConvert.SerializeObject(guids)}=>任务升级计划已存在或找不到升级对象");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 删除指定的升级记录
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public OperationResult DeleteUpgradeLog(CacheUser cacheUser, Guid[] Orgids, params Guid[] Ids)
        {
            int count = 0;
            UpgradeLogRepository.UnitOfWork.BeginTransaction();
            for (int n = 0; n < Ids.Length; n++)
            {
                Guid guid = Ids[n];
                if (UpgradeLogRepository.CheckExists(m => m.Id == guid))
                {
                    count += UpgradeLogRepository.Delete(m => m.Id == guid && (Orgids.Contains(m.Organize_Id) || cacheUser.IsAdministrator));
                }
            }
            UpgradeLogRepository.UnitOfWork.Commit();
            return new OperationResult(OperationResultType.Success, $"删除请求已执行,成功删除{count}条数据",new { count });
        }

        /// <summary>
        /// 设置指定升级记录的完成时间
        /// </summary>
        /// <param name="dynamics"></param>
        /// <returns></returns>
        public OperationResult EditUpgradeLog(params dynamic[] dynamics)
        {
            int count = 0;
            UpgradeLogRepository.UnitOfWork.BeginTransaction();
            for (int n = 0; n < dynamics.Length; n++)
            {
                Guid Id = dynamics[n].Id;
                int state = dynamics[n].State;
                var value = UpgradeLogRepository.TrackEntities.FirstOrDefault(m => m.Id == Id);
                if (value != null)
                {
                    value.CompleteTime = DateTime.Now;
                    value.State = (short)state;
                    count += UpgradeLogRepository.Update(value);
                }
            }
            UpgradeLogRepository.UnitOfWork.Commit();
            return new OperationResult(OperationResultType.Success, $"修改任务升级计划记录状态影响{count}条",count);
        }
    }
}
