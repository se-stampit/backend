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
        public ActionResult Index()
        {
           
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
        /*
        public void initRole()
        {
            setRole("Admin");
            setRole("Manager");
            setRole("KioskUser");
        }

        public void setRole(string rolename)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
           
            if (!Roles.RoleExists(rolename))
                Roles.CreateRole(rolename);
        }
        */
    }
}