using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Stampit.CommonType;

namespace Stampit.Service.Middleware
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context.Request.Headers.ContainsKey(Setting.AUTH_HEADER))
            {
                
            }
            return this.Next?.Invoke(context);
        }
    }
}