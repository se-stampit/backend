using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers.Authorization
{
    public class StampitAuthorize : AuthorizeAttribute
    {
        public string Name { get; }

        public StampitAuthorize(string name)
        {
            Name = name;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            return;
            base.OnAuthorization(filterContext);
        }
    }
}