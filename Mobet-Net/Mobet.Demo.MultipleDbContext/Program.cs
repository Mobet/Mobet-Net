using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Application.Services;
using Mobet.Infrastructure.Repositories;
using Mobet.Infrastructure;
using Mobet.Domain.Repositories;
using Mobet.Dependency;
using Mobet.Application;
using Mobet.Domain.UnitOfWork;
using Mobet.Configuration.Startup;

namespace Mobet.Demo.MultipleDbContext
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper boot = new Bootstrapper();
            boot.RegisterConsoleApplication();

            boot.UseDataAccessEntityFramework()
            .UseAutoMapper()
                ;
            var logService = IocManager.Instance.Resolve<ILogService>();
            var userAccountService = IocManager.Instance.Resolve<IUserAccountService>();
            var unitofwork = IocManager.Instance.Resolve<IUnitOfWorkManager>();

            using (var uow = unitofwork.Begin() )
            {
                userAccountService.Create();

                var response = logService.Create(new SoftwareDevelopmentKit.Log.LogCreateRequest
                {
                    Client = "MAC OS",
                    Duration = 0,
                    Host = "127.0.01",
                    Level = SoftwareDevelopmentKit.Log.AuditLevel.DEBUG,
                    Route = "UserAccount/GetPaging",
                    Time = DateTime.Now,
                    Message = "进入方法GetPaging...",
                    User = "Mobet"
                });

                uow.Complete();
            }
            

            

        }
    }
}
