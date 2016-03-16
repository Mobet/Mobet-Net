using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Models;
using Mobet.EntityFramework;

namespace Mobet.Infrastructure
{
    public class Repository<TEntity> : Repository<AuthorizationDbContext, TEntity>
        where TEntity : class,IEntity
    {
        public Repository(IEntityFrameworkDbContextProvider<AuthorizationDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
    public class Repository2<TEntity> : Repository<AuditDbContext, TEntity>
    where TEntity : class,IEntity
    {
        public Repository2(IEntityFrameworkDbContextProvider<AuditDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
