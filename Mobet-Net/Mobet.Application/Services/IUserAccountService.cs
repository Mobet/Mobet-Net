using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Dependency;
using Mobet.Domain.Services;
using Mobet.SoftwareDevelopmentKit.UserAccount;

namespace Mobet.Application.Services
{
    public interface IUserAccountService : IApplicationService
    {
        UserAccountGetPagingResponse GetPaging(UserAccountGetPagingRequest request);
        void Create();
    }
}
