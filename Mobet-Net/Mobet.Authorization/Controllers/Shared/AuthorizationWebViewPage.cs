using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobet.Authorization.Controllers.Shared
{
    public abstract class AuthorizationWebViewPage : AuthorizationWebViewPage<decimal>
    {
    }
    public abstract class AuthorizationWebViewPage<TModel> : AbstractWebViewPage<TModel>
    {
        protected AuthorizationWebViewPage()
        {
            LocalizationSourceName = "AccountService";
        }
    }
}