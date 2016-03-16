using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Repositories;
using Mobet.Domain.Models;
using Mobet.EntityFramework;

namespace Mobet.Infrastructure.Repositories
{
    public class LogRepository : Repository2<Log>, ILogRepository
    {
        public LogRepository(IEntityFrameworkDbContextProvider<AuditDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
