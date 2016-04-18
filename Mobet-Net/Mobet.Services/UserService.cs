using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;


using Mobet.AutoMapper;
using Mobet.Domain.UnitOfWork;
using Mobet.Domain.Entities;
using Mobet.Domain.Repositories;
using Mobet.Extensions;
using Mobet.Services.Services;
using Mobet.Services.Requests.User;
using Mobet.Localization;
using Mobet.Caching;

using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Extensions;
using Mobet.Utils;
using Mobet.Runtime.Security;
using Mobet.Net.Mail;

namespace Mobet.Services
{
    public class UserService : UserServiceBase, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ICacheManager cacheManager;
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public ILocalizationManager LocalizationManager { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="cacheManager"></param>
        /// <param name="unitOfWorkManager"></param>
        public UserService(IUserRepository userRepository, ICacheManager cacheManager, IUnitOfWorkManager unitOfWorkManager)
        {
            this.userRepository = userRepository;
            this.cacheManager = cacheManager;
            this.unitOfWorkManager = unitOfWorkManager;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserGetPagingResponse GetPaging(UserGetPagingRequest request)
        {
            int total = 0;
            var models = userRepository.Models
                            .WhereIf(!string.IsNullOrEmpty(request.Name), x => x.Name == request.Name)
                            .OrderBy(x => request.Sorting)
                            .Paging(request.PageIndex, request.PageSize, out total);

            return new UserGetPagingResponse { Total = total, Models = models.MapTo<IEnumerable<Models.User>>() };
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserCreateResponse Create(UserCreateRequest request)
        {
            if (userRepository.Any(x => x.Telphone == request.Telphone))
            {
                return new UserCreateResponse
                {
                    Result = false,
                    Message = LocalizationManager.GetSource(Constants.Localization.SourceName.Messages).GetString(Constants.Localization.MessageIds.UserAlreadyExists)
                };
            }
            var model = request.MapTo<User>();
            model.Salt = Guid.NewGuid().ToString().ToUpper();
            model.Subject = Guid.NewGuid().ToString().ToUpper();
            model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();
            userRepository.Add(model);
            return new UserCreateResponse
            {
                Result = true,
                Message = LocalizationManager.GetSource(Constants.Localization.SourceName.Messages).GetString(Constants.Localization.MessageIds.UserAddComplete)
            };
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserChangeResponse Change(UserCreateRequest request)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置用户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserSetPasswordResponse SetPassword(UserSetPasswordRequest request)
        {
            var model = userRepository.Find(request.Id.TryInt(0));

            if (model == null)
            {
                return new UserSetPasswordResponse(false, "未知用户");
            }

            if (model.Password != CryptoManager.EncryptMD5(request.OldPassword + model.Salt).ToUpper())
            {
                return new UserSetPasswordResponse(false, "原密码错误");
            }

            model.Password = CryptoManager.EncryptMD5(request.Password + model.Salt).ToUpper();

            userRepository.UpdateProperty(model, x => new { x.Password });

            return new UserSetPasswordResponse(true, "密码修改成功");
        }
        /// <summary>
        /// 设置用户邮箱
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserSetEmailResponse SetEmail(UserSetEmailRequest request)
        {
            var model = userRepository.Find(request.Id.TryInt(0));

            if (model == null)
            {
                return new UserSetEmailResponse(false, "未知用户");
            }
            userRepository.UpdateProperty(model, x => new { x.Email });

            return new UserSetEmailResponse(true, "邮箱修改成功");
        }
        /// <summary>
        /// 使用手机号注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserRegisterByTelphoneResponse RegisterByTelphone(UserRegisterByTelphoneRequest request)
        {
            var response = this.Create(request.MapTo<UserCreateRequest>());
            return new UserRegisterByTelphoneResponse
            {
                Result = response.Result,
                Message = response.Message
            };
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserGetProfileDataResponse GetUserProfileData(UserGetProfileDataRequest request)
        {
            var model = userRepository.FirstOrDefault(x => x.Subject == request.UserId);
            if (model == null)
            {
                return new UserGetProfileDataResponse();
            }
            return new UserGetProfileDataResponse { Model = model.MapTo<Models.User>() };
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                // issue the claims for the user
                string subject = context.Subject.GetSubjectId();
                var model = userRepository.FirstOrDefault(x => x.Subject == subject);
                if (model != null)
                {
                    var user = model.MapTo<Models.User>();

                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Id, user.Id.ToString()));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Name, user.NickName));
                    user.Claims.Add(new System.Security.Claims.Claim(IdentityServer3.Core.Constants.ClaimTypes.Subject, user.Subject));

                    context.IssuedClaims = user.Claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
                }

                return uow.CompleteAsync();
            }
        }

        /// <summary>
        /// 本地登录授权
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            using (var uow = unitOfWorkManager.Begin())
            {
                var model = userRepository.FirstOrDefault(x => x.Telphone == context.UserName || x.Email == context.UserName);

                if (model != null && model.Password == CryptoManager.EncryptMD5(context.Password + model.Salt).ToUpper())
                {
                    context.AuthenticateResult = new AuthenticateResult(model.Subject, model.Telphone);
                }

                return uow.CompleteAsync();
            }
        }

    }
}
