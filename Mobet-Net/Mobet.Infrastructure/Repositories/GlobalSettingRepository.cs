using Mobet.Domain.Repositories;
using Mobet.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Infrastructure.Repositories
{
    public class GlobalSettingRepository : Repository<ModelsContainer, Mobet.GlobalSettings.Models.GlobalSetting, Guid>, IGlobalSettingRepository
    {
        public GlobalSettingRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
