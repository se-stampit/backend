using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Stampit.CommonType;

namespace Stampit.Service.Middleware
{
    public class AuthorizationMiddleware : OwinMiddleware
    {
        public AuthorizationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var authMode = (RequestAuthenticationMode)context.Request.Environment[Setting.AUTH_MODE];

            if (IsLoginRequired(context?.Request?.Uri?.ToString().ToLower()))
            {
                if (authMode == RequestAuthenticationMode.SESSIONTOKEN)
                    await this.Next?.Invoke(context);
                else
                    await Task.FromException(new HttpException(401, "The given sessiontoken is not valid and theirfor /me/* can't be accessed"));
            }
            else
                await this.Next?.Invoke(context);
        }

        private bool IsLoginRequired(string url)
        {
            return url?.Contains("/api/me") ?? true;
        }
    }
}