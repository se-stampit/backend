using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Stampit.Service
{
	public static class UnityConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var container = new UnityContainer();
			
			//REGISTRATION
			
			config.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}