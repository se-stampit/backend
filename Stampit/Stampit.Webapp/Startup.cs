﻿using Microsoft.Owin;
using Owin;
using Stampit.Webapp.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(Stampit.Webapp.Startup))]
namespace Stampit.Webapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureAuth(app);
            new Stampit.Service.Startup().Configuration(app, UnityConfig.GetConfiguredContainer());
        }
    }
}
