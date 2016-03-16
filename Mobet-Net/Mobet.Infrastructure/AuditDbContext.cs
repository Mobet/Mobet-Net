using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Models;
using Mobet.EntityFramework;
using Castle.Core.Logging;

namespace Mobet.Infrastructure
{
    public class AuditDbContext : EntityFrameworkDbContext
    {
        public ILogger Logger { get; set; }
        public AuditDbContext()
            : base("Mobet.Audit")
        {
            Logger = NullLogger.Instance;
            Logger.DebugFormat("new an AuditDbContext");
        }
        public AuditDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Logger = NullLogger.Instance;
            Logger.DebugFormat("new an AuditDbContext with parameter {0}",nameOrConnectionString);
        }

        public virtual DbSet<Log> Log { get; set; }
    }
}
