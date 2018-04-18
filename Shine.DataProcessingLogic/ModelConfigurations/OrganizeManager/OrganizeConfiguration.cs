using Shine.Data.EF;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.DataProcessingLogic.ModelConfigurations.OrganizeManager
{
    /// <summary>
    /// <see cref="Organize"/>表数据实体映射
    /// </summary>
    public class OrganizeConfiguration : EntityConfigurationBase<Organize, Guid>
    {
        public OrganizeConfiguration()
        {
            this.HasOptional(m => m.Parent)
                .WithMany(n => n.Children)
                .HasForeignKey(m => m.ParentId);

            this.HasRequired(m => m.DataItemDetailOne)
                .WithMany()
                .HasForeignKey(m => m.DataItemDetail_Id);

            this.Property(m=>m.OrganizeLogo)
                .HasColumnType("image");
        }
    }
}
