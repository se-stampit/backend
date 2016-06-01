using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }

        // GET: StampCardStatistics
        public ActionResult CardsInCirculation()
        {
            return View();
        }

        // GET: StampCardStatistics
        public ActionResult CardStatus()
        {
            return View();
        }

        // GET: StampCardStatistics
        public ActionResult Sales()
        {
            return View();
        }
    }
}