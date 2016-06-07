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

namespace Stampit.Service.Middleware
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if(context.Request.Uri.AbsolutePath.ToLower().EndsWith("login"))
            {
                var loginprovider = await context.Request.Body.ConvertStreamToObject(JsonConvert.DeserializeObject<Loginprovider>);
                string sessionToken = await Login(loginprovider);
                Authenticate(context);
            }

            if (context.Request.Uri.AbsolutePath.ToLower().EndsWith("register"))
            {

            }

            context.Request.Body.Position = 0;
            this.Next?.Invoke(context);
        }

        public async Task<string> Login(Loginprovider provider)
        {
            return "";
        }

        public async Task<string> Register(Loginprovider provider)
        {
            return "";
        }

        public void Authenticate(IOwinContext context)
        {
            try
            {
                if (context.Request.Headers.ContainsKey(Setting.AUTH_HEADER))
                {
                    Oauth2Service service = new Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer());
                    Oauth2Service.TokeninfoRequest request = service.Tokeninfo();
                    request.AccessToken = context.Request.Headers[Setting.AUTH_HEADER];

                    Tokeninfo info = request.Execute();
                    context.Request.Environment[Setting.AUTH_ENVIRONMENT_ACCESSTOKEN] = context.Request.Headers[Setting.AUTH_HEADER];
                    context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID] = info.Email;
                }
                else
                {
                    context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID] = Setting.AUTH_ANONYMOUS;
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}