using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

using Mobet.EntityFramework;
using Mobet.Domain.Entities;
using Mobet.Configuration.Startup;
using Castle.Core.Logging;
using System.Data.Entity.ModelConfiguration;
using Mobet.Infrastructure.Mappings;
using Mobet.GlobalSettings.Models;

namespace Mobet.Infrastructure
{
    public class ModelsContainer : EntityFrameworkDbContext
    {
        public ModelsContainer() : base("Mobet.Authorization")
        {

        }

        public ModelsContainer(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new UserRolesMapping());
            builder.Configurations.Add(new RoleMenusMapping());

            base.OnModelCreating(builder);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSettings { get; set; }
        

    }
}
