using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Stampit.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app, IUnityContainer container)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            config.EnsureInitialized();
            app.UseWebApi(config);
        }
    }
}