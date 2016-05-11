using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stampit.Service.Middleware
{
    public static class MiddlewareExtension
    {
        public static IAppBuilder UseAuthentication(this IAppBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Use<AuthenticationMiddleware>();

            return builder;
        }

        public static IAppBuilder UseAuthorization(this IAppBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Use<AutorizationMiddleware>();

            return builder;
        }
    }
}