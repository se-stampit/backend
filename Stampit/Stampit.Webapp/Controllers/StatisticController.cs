﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.SimpleChart;
using Stampit.Logic.Interface;
using System.Threading.Tasks;

namespace Stampit.Webapp.Controllers
{
    public class StatisticController : Controller
    {

        private IProductRepository ProductRepository { get; }
        private ICompanyRepository CompanyRepository { get; }
        private IEnduserRepository EnduserRepository { get; }

        public StatisticController(IProductRepository productRepository, ICompanyRepository companyRepository, IEnduserRepository enduserRepository)
        {
            this.ProductRepository = productRepository;
            this.CompanyRepository = companyRepository;
            this.EnduserRepository = enduserRepository;
        }

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

        // GET: CardStatusStatistics
        public ActionResult CardStatus()
        {
            return PartialView();
        }

        // GET: SalesStatistics
        public async Task<ActionResult> Sales()
        {
            var salesProduct = await ProductRepository.SalesPerProduct(null); //TODO!

            var sales = from p in salesProduct
                    select new SimpleData {
                        Value = p.Value,
                        Color = "#F7464A",
                        Highlight = "#FF5A5E",
                        Label = p.Key.Productname
                    };
            return PartialView(sales);
        }

        // GET: CustomerStatistics
        public async Task<ActionResult> Customer()
        {
            var countEnduser = await EnduserRepository.CountEnduser(null); //TODO!
            return PartialView(countEnduser);
        }
    }
}