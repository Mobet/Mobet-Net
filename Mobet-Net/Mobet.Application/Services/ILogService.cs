using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Services;
using Mobet.SoftwareDevelopmentKit.Log;

namespace Mobet.Application.Services
{
    public interface ILogService : IApplicationService
    {
        LogCreateResponse Create(LogCreateRequest request);
    }
}
