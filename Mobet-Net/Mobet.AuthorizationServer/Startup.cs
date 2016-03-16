using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core;
using Mobet.AuthorizationServer.Configuration;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WeChat;
using Microsoft.Owin.Security.QQ;

[assembly: OwinStartup(typeof(Mobet.Mobet.AuthorizationServer.Startup))]
namespace Mobet.Mobet.AuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
               {
                   SiteName = "IdentityServer3 -  Identity",
                   SigningCertificate = Certificate.Get(),

                   Factory = Factory.Configure("Mobet.OAuth2AuthorizationServer"),

                   AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                   {
                       EnablePostSignOutAutoRedirect = true,
                       IdentityProviders = ConfigureIdentityProviders
                   }
               });
            });
        }
        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseQQConnectAuthentication(new QQConnectAuthenticationOptions
            {
                AuthenticationType = "QQ",
                Caption = "Sign-in with QQ",
                SignInAsAuthenticationType = signInAsType,

                AppId = "wx1ef4827bc8bc7a47",
                AppSecret = "4f2229bfefd96aa9d527b1ea1da657b4 "
            });

            app.UseWeChatAuthentication(new WeChatAuthenticationOptions
            {
                AuthenticationType = "WeChat",
                Caption = "Sign-in with WeChat",
                SignInAsAuthenticationType = signInAsType,

                AppId = "wx1ef4827bc8bc7a47",
                AppSecret = "4f2229bfefd96aa9d527b1ea1da657b4 "
            });
        }

    }
}