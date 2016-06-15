using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stampit.Service.Middleware
{
    public class ExceptionHandlerMiddleware : OwinMiddleware
    {
        public ExceptionHandlerMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await this.Next?.Invoke(context);
            }
            catch (HttpException ex)
            {
                try
                {
                    await GenerateExceptionMessage(context, ex.Message, ex.GetHttpCode());
                    return;
                }
                catch { }
                throw;
            }
            catch (Exception ex)
            {
                try
                {
                    await GenerateExceptionMessage(context, ex.Message, 500);
                    return;
                }
                catch { }
                throw;
            }
        }

        private async Task GenerateExceptionMessage(IOwinContext context, string message, int statuscode)
        {
            context.Response.StatusCode = statuscode;
            context.Response.ReasonPhrase = ((System.Net.HttpStatusCode)statuscode).ToString();
            context.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(
                new
                {
                    code = statuscode.ToString().Substring(0,1),
                    message = message
                }
            );
            context.Response.ContentLength = json.ToCharArray().LongLength;
            await context.Response.WriteAsync(json);
            await context.Response.Body.FlushAsync();
        }
    }
}