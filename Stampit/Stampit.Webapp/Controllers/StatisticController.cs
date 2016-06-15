using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.SimpleChart;
using Stampit.Logic.Interface;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Webapp.Controllers
{
    public class StatisticController : Controller
    {

        private IProductRepository ProductRepository { get; }
        private ICompanyRepository CompanyRepository { get; }
        private IEnduserRepository EnduserRepository { get; }
        private IStampcardRepository StampcardRepository { get; }

        public StatisticController(IProductRepository productRepository, ICompanyRepository companyRepository, IEnduserRepository enduserRepository, IStampcardRepository stampcardRepository)
        {
            this.ProductRepository = productRepository;
            this.CompanyRepository = companyRepository;
            this.EnduserRepository = enduserRepository;
            this.StampcardRepository = stampcardRepository;
        }

        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }

        // GET: StampCardStatistics
        public async Task<ActionResult> CardsInCirculation()
        {
            var total = await StampcardRepository.CountStampcardsFromCompany(null); //TODO
            var redeemed = await StampcardRepository.CountRedeemedStampcardsFromCompany(null); //TODO
            var unredeemed = total-redeemed;
            List<SimpleData> data = new List<SimpleData> {
                new SimpleData
                {
                    Value = redeemed,
                    Color = "#46BFBD",
                    Highlight = "#5AD3D1",
                    Label = "RedeemedCards"
                },
                new SimpleData
                {
                    Value = unredeemed,
                    Color = "#FDB45C",
                    Highlight = "#FFC870",
                    Label = "UnredeemedCards"
                }

            };
            return PartialView(data);
        }

        // GET: CardStatusStatistics
        public async Task<ActionResult> CardStatus()
        {
            var stampcardsCompany = await StampcardRepository.GetAllStampcardsFromCompany(null); //TODO;

            var stampcards = from s in stampcardsCompany
                             select new BarChartDTO
                             {
                                 BarChart = new BarChart
                                 {
                                     ComplexData = new ComplexData
                                     {
                                         Labels = (from data in s.Value
                                                   select data.Key.ToString()).ToList(),
                                         Datasets = (from data in s.Value
                                                     select new ComplexDataset
                                                     {
                                                         Data = s.Value.Values.Select(val => (double)val).ToList(),
                                                         FillColor = "rgba(220,220,220,0.2)"
                                                     }).Take(1).ToList()
                                     }
                                 },
                                Product = s.Key
                             };
            return PartialView(stampcards);
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

    public class BarChartDTO
    {
        public Product Product { get; set; }
        public BarChart BarChart { get; set; }
    }
}