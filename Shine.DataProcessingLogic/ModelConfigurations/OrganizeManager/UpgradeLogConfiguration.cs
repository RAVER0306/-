using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    public class UpgradeLogConfiguration : EntityConfigurationBase<UpgradeLog, Guid>
    {
        public UpgradeLogConfiguration()
        {
            this.HasRequired(m => m.OrganizeOne)
                .WithMany()
                .HasForeignKey(m => m.Organize_Id)
                .WillCascadeOnDelete(true);
        }
    }
}
