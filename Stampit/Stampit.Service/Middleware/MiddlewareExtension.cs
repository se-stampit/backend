using Microsoft.Owin;
using Owin;
using Stampit.CommonType;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stampit.Service.Middleware
{
    public static class MiddlewareExtension
    {
        public static IAppBuilder UseAuthentication(this IAppBuilder builder, IAuthenticationTokenStorage authTokens)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Use<AuthenticationMiddleware>(authTokens);

            return builder;
        }

        public static IAppBuilder UseAuthorization(this IAppBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Use<AuthorizationMiddleware>();

            return builder;
        }

        public static IAppBuilder UseExceptionHandler(this IAppBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Use<ExceptionHandlerMiddleware>();

            return builder;
        }

        public static RequestAuthenticationMode GetAuthenticationMode(this IOwinContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!context.Request.Uri.AbsolutePath.ToLower().Contains("/api/"))
                return RequestAuthenticationMode.NONE;

            if (context.Request.Uri.AbsolutePath.ToLower().EndsWith("register"))
                return RequestAuthenticationMode.REGISTER;
            if (context.Request.Uri.AbsolutePath.ToLower().EndsWith("login"))
                return RequestAuthenticationMode.LOGIN;
            if (context.Request.Headers.ContainsKey(Setting.AUTH_HEADER))
                return RequestAuthenticationMode.SESSIONTOKEN;

            return RequestAuthenticationMode.NONE;
        }
    }
}