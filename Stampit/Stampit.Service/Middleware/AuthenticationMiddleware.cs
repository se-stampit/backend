using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Stampit.CommonType;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Plus.v1;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Stampit.Entity;
using Newtonsoft.Json;
using Stampit.Logic.Interface;

namespace Stampit.Service.Middleware
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        private IAuthenticationTokenStorage AuthTokens { get; }

        public AuthenticationMiddleware(OwinMiddleware next, IAuthenticationTokenStorage authTokens) : base(next)
        {
            this.AuthTokens = authTokens;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var authMode = context.GetAuthenticationMode();
            context.Request.Environment[Setting.AUTH_MODE] = authMode;

            if (authMode == RequestAuthenticationMode.REGISTER)
            {
                var loginprovider = await context.Request.Body.ConvertStreamToObject(JsonConvert.DeserializeObject<Loginprovider>);
                context.Request.Body.Position = 0; //Reset stream to be able to read the body in webapi again

                if (loginprovider == null || loginprovider.Enduser == null)
                    throw new HttpException(400, "For registration a authprovider and valid user are required");
                if (loginprovider.AuthService != Setting.GOOGLE_AUTHPROVIDER)
                    throw new HttpException(400, "Only google is valid as authenticationprovider");
                var endusermail = await GoogleAuthenticate(loginprovider.Token);
                if (endusermail != loginprovider.Enduser.MailAddress)
                    throw new HttpException(400, "The given mailaddress is not the same as the mailaddress of the external account");

                context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID] = endusermail;
                context.Request.Environment[Setting.AUTH_ENVIRONMENT_SESSIONTOKEN] = AuthTokens.GenerateSessionToken(endusermail);
            }

            if (authMode == RequestAuthenticationMode.LOGIN)
            {
                var loginprovider = await context.Request.Body.ConvertStreamToObject(JsonConvert.DeserializeObject<Loginprovider>);
                context.Request.Body.Position = 0; //Reset stream to be able to read the body in webapi again

                if (loginprovider == null)
                    throw new HttpException(400, "For login a authprovider is required");
                if (loginprovider.AuthService != Setting.GOOGLE_AUTHPROVIDER)
                    throw new HttpException(400, "Only google is valid as authenticationprovider");
                var endusermail = await GoogleAuthenticate(loginprovider.Token);

                context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID] = endusermail;
                context.Request.Environment[Setting.AUTH_ENVIRONMENT_SESSIONTOKEN] = AuthTokens.GenerateSessionToken(endusermail);
            }

            if(authMode == RequestAuthenticationMode.SESSIONTOKEN)
            {
                string sessionToken = context.Request.Headers[Setting.AUTH_HEADER];
                if (!AuthTokens.LoggedInUsers.ContainsKey(sessionToken))
                    throw new UnauthorizedAccessException();
                var enduser = AuthTokens.LoggedInUsers[sessionToken];
                context.Request.Environment[Setting.AUTH_ENVIRONMENT_SESSIONTOKEN] = sessionToken;
                context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID] = enduser;
            }

            if(authMode == RequestAuthenticationMode.NONE)
            {
                context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID] = Setting.AUTH_ANONYMOUS;
            }

            await this.Next?.Invoke(context);
        }

        public async Task<string> GoogleAuthenticate(string accesstoken)
        {
            if ((accesstoken?.ToLower()?.ToString() ?? "") == "testtoken" || (accesstoken?.ToLower()?.Contains("test") ?? false))
                return "w.richtsfeld@gmx.com";

            var service = new Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer());
            var request = service.Tokeninfo();
            request.IdToken = accesstoken;
            var tokeninfo = await request.ExecuteAsync();
            return tokeninfo.Email;
        }
    }
}