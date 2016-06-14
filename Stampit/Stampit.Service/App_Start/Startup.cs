﻿using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Stampit.Service.Middleware;
using Newtonsoft.Json;
using Stampit.Logic.Interface;

namespace Stampit.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app, IUnityContainer container)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFF"
            };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Stampit.Entity.IsoDateTimeWithoutPlusConverter());
            config.EnsureInitialized();

            app.UseAuthentication(container.Resolve<IAuthenticationTokenStorage>());
            //app.UseAuthorization();
            app.UseWebApi(config);
        }
    }
}