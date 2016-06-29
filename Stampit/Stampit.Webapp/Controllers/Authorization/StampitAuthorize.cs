using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    public class StampitAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string role = httpContext.Session["role"]?.ToString().ToLower() ?? "none";

            string[] allowedRoles = Roles.Split(',').Select(r => r.ToLower()).ToArray();

            return allowedRoles.Contains(role) || role == "admin";
        }
    }
}