using Shine.DataProcessingLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.Core.Data;

namespace Shine.DataProcessingLogic.Services
{

    public class ElectricityService : IElectricityContract
    {
        #region 数据仓储
        public IRepository<AnnualElectricity, Guid> AnnualRepository { set; get; }

        public IRepository<DayElectricity, Guid> DayRepository { set; get; }

        public IRepository<MonthElectricity, Guid> MonthRepository { set; get; }
        #endregion

        #region 接口实现
        /// <summary>
        /// <see cref="AnnualElectricity"/>表预查询数据
        /// </summary>
        public IQueryable<AnnualElectricity> AnnualElectricityQueryable => AnnualRepository.Entities;

        /// <summary>
        /// <see cref="DayElectricity"/>表预查询数据
        /// </summary>
        public IQueryable<DayElectricity> DayElectricityQueryable => DayRepository.Entities;

        /// <summary>
        /// <see cref="MonthElectricity"/>表预查询数据
        /// </summary>
        public IQueryable<MonthElectricity> MonthElectricityQueryable => MonthRepository.Entities;
        #endregion
    }
}
