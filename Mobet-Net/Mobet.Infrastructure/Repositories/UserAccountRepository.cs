using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain;
using Mobet.Domain.Models;
using Mobet.Domain.Repositories;
using Mobet.EntityFramework;

namespace Mobet.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(IEntityFrameworkDbContextProvider<AuthorizationDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            
        }
    }
}
