using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Application.Services;
using Mobet.Domain.Repositories;
using Mobet.SoftwareDevelopmentKit.Log;
using Mobet.AutoMapper;
using Mobet.Domain.Models;

namespace Mobet.Application
{
    public class LogService : ILogService
    {
        public ILogRepository LogRepository { get; set; }
        public LogService(ILogRepository _LogRepository)
        {
            LogRepository = _LogRepository;
        }

        public LogCreateResponse Create(LogCreateRequest request)
        {
            LogRepository.Add(request.MapTo<Log>());
            return new LogCreateResponse { Result = true, Message = "添加日志成功" };
        }
    }
}
