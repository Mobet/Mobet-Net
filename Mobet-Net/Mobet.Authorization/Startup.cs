using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core;
using Mobet.Authorization.Configuration;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WeChat;
using Microsoft.Owin.Security.QQ;
using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Protocols;
using System.Threading.Tasks;
using Mobet.Localization;
using IdentityServer3.WsFederation.Configuration;
using IdentityServer3.WsFederation.Models;
using IdentityServer3.WsFederation.Services;
using Microsoft.Owin.Security.WsFederation;

[assembly: OwinStartup(typeof(Mobet.Authorization.Startup))]
namespace Mobet.Authorization
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    IssuerUri = "https://localhost:44373/",                                             //令牌颁发者Uri
                    SiteName = "IdentityServer3 -  Identity",                                           //站点名称
                    SigningCertificate = Certificate.Get(),                                             //X.509证书（和相应的私钥签名的安全令牌）
                    RequireSsl = true,                                                                  //必须为SSL,默认为True
                    Endpoints = new EndpointOptions                                                     //允许启用或禁用特定的端点（默认的所有端点都是启用的）。
                    {
                        EnableCspReportEndpoint = false
                    },
                    Factory = IdentityServer3Factory.Configure("Mobet.Authorization"),                  //自定义配置
                    PluginConfiguration = PluginConfiguration,                                          //插件配置,允许添加协议插件像WS联邦支持。
                    ProtocolLogoutUrls = new List<string>                                               //配置回调URL，应该叫中登出（主要协议插件有用）。
                    {

                    },
                    LoggingOptions = new LoggingOptions                                                 //日志配置
                    {

                    },
                    CspOptions = new CspOptions
                    {
                        Enabled = false,
                    },
                    EnableWelcomePage = true,                                                            //启用或禁用默认的欢迎页。默认为True
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions //授权配置
                    {
                        EnablePostSignOutAutoRedirect = true,
                        IdentityProviders = ConfigureIdentityProviders,
                        LoginPageLinks = new List<LoginPageLink> {
                            new LoginPageLink{ Text = "立即注册", Href = "/Account/Registration"},
                            new LoginPageLink{ Text = "忘记密码？", Href = "/Account/ForgotPassword"}
                       }
                    },

                    EventsOptions = new EventsOptions                                                   //事件配置
                    {
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true,
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true
                    }
                });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });


            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            //{
            //    ClientId = "mvc.owin.hybrid",                                   //客户端ID,客户端唯一标识
            //    Authority = "https://localhost:44373/core",                     //权限验证地址
            //    RedirectUri = "https://localhost:44373/",                       //验证成功后返回地址，此地址在申请客户端时填写
            //    PostLogoutRedirectUri = "https://localhost:44373/",             //登出后返回地址
            //    ResponseType = "code id_token",                                 //授权响应类型
            //    Scope = "openid profile read write offline_access",             //授权范围


            //    SignInAsAuthenticationType = "Cookies",

            //    Notifications = new OpenIdConnectAuthenticationNotifications
            //    {
            //        AuthorizationCodeReceived = async n =>
            //        {
            //            // use the code to get the access and refresh token
            //            var tokenClient = new TokenClient(
            //                "https://localhost:44373/core/connect/token",
            //                "mvc.owin.hybrid",
            //                "secret");

            //            var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(
            //                n.Code, n.RedirectUri);

            //            // use the access token to retrieve claims from userinfo
            //            var userInfoClient = new UserInfoClient(
            //                new Uri("https://localhost:44373/core/connect/userinfo"),
            //                tokenResponse.AccessToken);

            //            var userInfoResponse = await userInfoClient.GetAsync();

            //            // create new identity
            //            var id = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType);
            //            id.AddClaims(userInfoResponse.GetClaimsIdentity().Claims);

            //            id.AddClaim(new Claim("access_token", tokenResponse.AccessToken));
            //            id.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToLocalTime().ToString()));
            //            id.AddClaim(new Claim("refresh_token", tokenResponse.RefreshToken));
            //            id.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
            //            id.AddClaim(new Claim("sid", n.AuthenticationTicket.Identity.FindFirst("sid").Value));

            //            n.AuthenticationTicket = new AuthenticationTicket(
            //                new ClaimsIdentity(id.Claims, n.AuthenticationTicket.Identity.AuthenticationType),
            //                n.AuthenticationTicket.Properties);
            //        },

            //        RedirectToIdentityProvider = n =>
            //        {
            //            // if signing out, add the id_token_hint
            //            if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
            //            {
            //                var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

            //                if (idTokenHint != null)
            //                {
            //                    n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
            //                }
            //            }

            //            return Task.FromResult(0);
            //        }
            //    }
            //});

            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                MetadataAddress = "https://localhost:44373/core/wsfed/metadata",
                Wtrealm = "urn:owinrp",
                SignInAsAuthenticationType = "Cookies"
            });
        }

        private void PluginConfiguration(IAppBuilder pluginApp, IdentityServerOptions options)
        {
            var wsFedOptions = new WsFederationPluginOptions(options);

            // data sources for in-memory services
            wsFedOptions.Factory.Register(new Registration<IEnumerable<RelyingParty>>(RelyingParties.Get()));
            wsFedOptions.Factory.RelyingPartyService = new Registration<IRelyingPartyService>(typeof(InMemoryRelyingPartyService));

            pluginApp.UseWsFederationPlugin(wsFedOptions);
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseQQConnectAuthentication(new QQConnectAuthenticationOptions
            {
                AuthenticationType = "QQ",
                Caption = "QQ登录",
                SignInAsAuthenticationType = signInAsType,

                AppId = "wx1ef4827bc8bc7a47",
                AppSecret = "4f2229bfefd96aa9d527b1ea1da657b4 "
            });

            app.UseWeChatAuthentication(new WeChatAuthenticationOptions
            {
                AuthenticationType = "WeChat",
                Caption = "微信登录",
                SignInAsAuthenticationType = signInAsType,

                AppId = "wx1ef4827bc8bc7a47",
                AppSecret = "4f2229bfefd96aa9d527b1ea1da657b4 "
            });
        }

    }
}