﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.SimpleChart;

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
            List<SimpleData> data = new List<SimpleData> {

                new SimpleData
                    {
                        Value = 300,
                        Color = "#F7464A",
                        Highlight = "#FF5A5E",
                        Label = "Red"
                    },
                    new SimpleData
                    {
                        Value = 50,
                        Color = "#46BFBD",
                        Highlight = "#5AD3D1",
                        Label = "Green"
                    },
                    new SimpleData
                    {
                        Value = 100,
                        Color = "#FDB45C",
                        Highlight = "#FFC870",
                        Label = "Yellow"
                    }

            };
            return PartialView(data);
        }

        // GET: StampCardStatistics
        public ActionResult CardStatus()
        {
            return PartialView();
        }

        // GET: StampCardStatistics
        public ActionResult Sales()
        {
            return View();
        }
    }
}