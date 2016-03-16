using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

using Mobet.EntityFramework;
using Mobet.Domain.Models;
using Mobet.Configuration.Startup;
using Castle.Core.Logging;

namespace Mobet.Infrastructure
{
    public class AuthorizationDbContext : EntityFrameworkDbContext
    {
        public ILogger Logger { get; set; }
        public AuthorizationDbContext()
            :base("Mobet.Authorization")
        {
            Logger = NullLogger.Instance;
            Logger.DebugFormat("new an AuthorizationDbContext");
        }

        public AuthorizationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

            Logger = NullLogger.Instance;
            Logger.DebugFormat("new an AuthorizationDbContext with parameter {0}", nameOrConnectionString);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
    }
}
