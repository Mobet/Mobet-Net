using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.AutoMapper;
using Mobet.Application.Services;
using Mobet.Domain.Repositories;
using Mobet.Extensions;
using Mobet.Application.Requests.User;
using System.ComponentModel;
using Mobet.Localization;
using Mobet.Caching;
using Mobet.Application.Enum;

namespace Mobet.Application
{
    public class UserService : IUserService
    {
        [Description("用户仓储")]
        public IUserRepository UserRepository { get; set; }

        public ILocalizationManager LocalizationManager { get; set; }

        public ICache CacheManager { get; set; }

        [Description("构造函数")]
        public UserService(IUserRepository userRepository, ICache cacheManager)
        {
            UserRepository = userRepository;
            CacheManager = cacheManager;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        [Description("分页查询")]
        public UserGetPagingResponse GetPaging(UserGetPagingRequest request)
        {
            int total = 0;
            var models = UserRepository.Models
                            .WhereIf(!string.IsNullOrEmpty(request.Name), x => x.Name == request.Name)
                            .OrderBy(x => request.Sorting)
                            .Paging(request.PageIndex, request.PageSize, out total);

            return new UserGetPagingResponse { Total = total, Models = models.MapTo<IEnumerable<Models.User>>() };
        }
        [Description("添加用户")]
        public UserCreateResponse Create(UserCreateRequest request)
        {
            throw new NotImplementedException();
        }
        [Description("修改用户信息")]
        public UserChangeResponse Change(UserCreateRequest request)
        {
            throw new NotImplementedException();
        }
        [Description("使用手机号注册")]
        public UserRegisterByMobilephoneResponse RegisterByMobilephone(UserRegisterByMobilephoneRequest request)
        {
            var code = CacheManager.Get<string>(string.Format(Constants.Cache.MessageAuthCode, MessageAuthCodeType.Register, request.Mobilephone, request.MessageAuthCode));

            if (!request.MessageAuthCode.Equals(code))
            {
                return new UserRegisterByMobilephoneResponse
                {
                    Result = false,
                    Message = Constants.AccountService.RegisterByMobilephoneInvalidMessageAuthCode
                };
            }

            var response = this.Create(request.MapTo<UserCreateRequest>());

            return new UserRegisterByMobilephoneResponse
            {
                Result = response.Result,
                Message = response.Message
            };
        }
    }
}
