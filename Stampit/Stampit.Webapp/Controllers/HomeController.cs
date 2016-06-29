using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Stampit.Webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Stampit.Webapp.Controllers
{
    public class HomeController : Controller
    {
        private const string SESSION_USER = "userID";
        private const string SESSION_COMPANY = "companyID";
        private const string SESSION_ROLE = "role";

        public ActionResult Index()
        {
            if (Session[SESSION_ROLE] == null)
                Session[SESSION_ROLE] = "None";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}