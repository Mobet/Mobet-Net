using Mobet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Infrastructure.Mappings
{
    public class RoleMenusMapping : EntityTypeConfiguration<Role>
    {
        public RoleMenusMapping()
        {
            this.HasMany(t => t.Menus).WithMany(x => x.Roles).Map(m =>
            {
                m.ToTable("RoleMenus");
                m.MapLeftKey("RoleId");
                m.MapRightKey("MenuId");
            });
        }
    }
}
