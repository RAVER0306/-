using Shine.Comman.Data;
using Shine.Core.Caching.Models;
using Shine.Core.Dependency;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.HostManager;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Linq;

namespace Shine.DataProcessingLogic.Contracts
{
    /// <summary>
    /// 能耗服务接口
    /// </summary>
    public interface IElectricityContract : IScopeDependency
    {
        /// <summary>
        /// <see cref="AnnualElectricity"/>表预查询数据
        /// </summary>
        IQueryable<AnnualElectricity> AnnualElectricityQueryable { get; }

        /// <summary>
        /// <see cref="DayElectricity"/>表预查询数据
        /// </summary>
        IQueryable<DayElectricity> DayElectricityQueryable { get; }

        /// <summary>
        /// <see cref="MonthElectricity"/>表预查询数据
        /// </summary>
        IQueryable<MonthElectricity> MonthElectricityQueryable { get; }
    }
}
