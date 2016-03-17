using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Dependency;
using Mobet.Domain.Services;
using Mobet.Application.Requests.User;

namespace Mobet.Application.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserGetPagingResponse GetPaging(UserGetPagingRequest request);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserCreateResponse Create(UserCreateRequest request);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserChangeResponse Change(UserCreateRequest request);
        /// <summary>
        /// 使用手机号码注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UserRegisterByMobilephoneResponse RegisterByMobilephone(UserRegisterByMobilephoneRequest request);
    }
}
