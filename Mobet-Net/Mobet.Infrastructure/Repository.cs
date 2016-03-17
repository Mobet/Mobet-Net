using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Models;
using Mobet.EntityFramework;

namespace Mobet.Infrastructure
{
    public class Repository<TEntity> : Repository<ModelsContainer, TEntity>
        where TEntity : class,IEntity
    {
        public Repository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
