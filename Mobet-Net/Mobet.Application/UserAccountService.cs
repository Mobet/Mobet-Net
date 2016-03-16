using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.AutoMapper;
using Mobet.Application.Services;
using Mobet.Domain.Repositories;
using Mobet.Extensions;
using Mobet.SoftwareDevelopmentKit.UserAccount;

namespace Mobet.Application
{
    public class UserAccountService : IUserAccountService
    {
        public IUserAccountRepository UserAccountRepository { get; set; }
        public UserAccountService(IUserAccountRepository _UserAccountRepository)
        {
            UserAccountRepository = _UserAccountRepository;
        }

        public UserAccountGetPagingResponse GetPaging(UserAccountGetPagingRequest request)
        {
            int total = 0;
            var models = UserAccountRepository.Models
                            .WhereIf(!string.IsNullOrEmpty(request.Name), x => x.Name == request.Name)
                            .OrderBy(x => request.OrderBy)
                            .Paging(request.PageIndex, request.PageSize, out total);
            return new UserAccountGetPagingResponse { Total = total, Models = models.MapTo<IEnumerable<SoftwareDevelopmentKit.Models.UserAccount>>() };
        }

        public void Create()
        {

            UserAccountRepository.Add(new Domain.Models.UserAccount
            {
                Name = "Ming",
                Account = "Ming@mobet.com"
            });
        }
    }
}
