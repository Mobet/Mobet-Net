using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;


using Mobet.AutoMapper;
using Mobet.Domain.UnitOfWork;
using Mobet.Domain.Repositories;
using Mobet.Extensions;
using Mobet.Services.Services;
using Mobet.Services.Enum;
using Mobet.Services.Requests.User;
using Mobet.Localization;
using Mobet.Caching;

using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Extensions;

namespace Mobet.Services
{
    public class UserService : UserServiceBase, IUserService
    {
        private IUserRepository _userRepository { get; set; }

        private ILocalizationManager _localizationManager { get; set; }

        private ICacheManager _cacheManager { get; set; }

        private IUnitOfWorkManager _unitOfWorkManager { get; set; }

        public ILocalizationManager LocalizationManager { get; set; }


        [Description("构造函数")]
        public UserService(IUserRepository userRepository, ICacheManager cacheManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _userRepository = userRepository;
            _cacheManager = cacheManager;
            _unitOfWorkManager = unitOfWorkManager;
            _localizationManager = NullLocalizationManager.Instance;
        }

        [Description("分页查询")]
        public UserGetPagingResponse GetPaging(UserGetPagingRequest request)
        {
            int total = 0;
            var models = _userRepository.Models
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
            var code = _cacheManager.Get<string>(string.Format(Constants.Cache.MessageCode, MessageAuthCodeType.Register, request.Mobilephone, request.MessageAuthCode));

            if (!request.MessageAuthCode.Equals(code))
            {
                return new UserRegisterByMobilephoneResponse
                {
                    Result = false,
                    Message = LocalizationManager.GetSource(Constants.Localization.SourceName.Messages).GetString(Constants.Localization.MessageIds.InvalidMessageCode)
                };
            }

            var response = this.Create(request.MapTo<UserCreateRequest>());

            return new UserRegisterByMobilephoneResponse
            {
                Result = response.Result,
                Message = response.Message
            };
        }

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {

            using (var uow = _unitOfWorkManager.Begin())
            {
                var model = _userRepository.FirstOrDefault(x => (x.Telphone == context.UserName && x.Password == context.Password)
                                                       || (x.Email == context.UserName && x.Password == context.Password)
                                                     );

                if (model != null)
                {
                    context.AuthenticateResult = new AuthenticateResult(model.Subject, model.Telphone);
                }

                return uow.CompleteAsync();
            }
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                // issue the claims for the user
                var model = _userRepository.FirstOrDefault(x => x.Subject == context.Subject.GetSubjectId());
                if (model != null)
                {
                    var user = model.MapTo<Models.User>();
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Name, user.Name));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.NickName, user.NickName));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Subject, user.Subject));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Id, user.Id.ToString()));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.BirthDate, user.Birthday.ToString()));

                    context.IssuedClaims = user.Claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
                }

                return uow.CompleteAsync();
            }
        }
    }
}
