using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    public class UpgradePackagesConfiguration : EntityConfigurationBase<UpgradePackages, Guid>
    {
        public UpgradePackagesConfiguration()
        {
            this.Property(m => m.Datas)
                .HasColumnType("image");


            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);
        }
    }
}
