using Mobet.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobet.Domain.Repositories
{
    public interface IGlobalSettingRepository : IRepository<Mobet.GlobalSettings.Models.GlobalSetting,Guid>
    {

    }
}
