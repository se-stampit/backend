using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Stampit.Service.Middleware
{
    public class AutorizationMiddleware : OwinMiddleware
    {
        public AutorizationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if(IsLoginRequired(context?.Request?.Uri?.ToString().ToLower()))
            {

            }
            await this.Next?.Invoke(context);
        }

        private bool IsLoginRequired(string url)
        {
            return url?.Contains("/api/me") ?? true;
        }
    }
}