using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="SubAggregation"/>表数据实体映射
    /// </summary>
    public class SubAggregationConuration:EntityConfigurationBase<SubAggregation,Guid>
    {
        public SubAggregationConuration()
        {
            //机构
            this.HasRequired(m => m.OrganizeOne)
                .WithMany(m => m.SubAggregationMany)
                .HasForeignKey(m => m.Organzie_Id);
            //主机
            this.HasRequired(m => m.HostOne)
                .WithMany(m => m.SubAggregationMany)
                .HasForeignKey(m => m.Host_Id);
            //灯杆
            this.HasRequired(m => m.LightPoleOne)
                .WithMany(m => m.SubAggregationMany)
                .HasForeignKey(m => m.LightPole_Id);
            //分控
            this.HasRequired(m => m.SubControlOne)
                .WithMany(m => m.SubAggregationMany)
                .HasForeignKey(m => m.SubControl_Id)
                .WillCascadeOnDelete(true);


        }

    }
}
