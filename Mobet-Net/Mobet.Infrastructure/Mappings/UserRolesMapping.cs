using Mobet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Infrastructure.Mappings
{
    public class UserRolesMapping : EntityTypeConfiguration<User>
    {
        public UserRolesMapping()
        {
            this.HasMany(t => t.Roles).WithMany(x => x.Users).Map(m =>
            {
                m.ToTable("UserRoles");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });
        }
    }
}
