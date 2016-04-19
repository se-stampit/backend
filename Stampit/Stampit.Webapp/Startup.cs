using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stampit.Webapp.Startup))]
namespace Stampit.Webapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
