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
using Stampit.CommonType;

namespace Stampit.Webapp.Controllers
{
    [StampitAuthorize(Roles = "Manager")]
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
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: StampCardStatistics
        public async Task<ActionResult> CardsInCirculation()
        {
            var currentCompany = await CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString());
            var total = await StampcardRepository.CountStampcardsFromCompany(currentCompany); //TODO
            var redeemed = await StampcardRepository.CountRedeemedStampcardsFromCompany(currentCompany); //TODO
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
            var currentCompany = await CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString());
            var stampcardsCompany = await StampcardRepository.GetAllStampcardsFromCompany(currentCompany); //TODO;

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

            /*
             * Data = new List<double> { 65, 59, 80, 81, 56, 55, 40 },
                                      Label = "My First dataset",
                                      FillColor = "rgba(220,220,220,0.2)",
                                      StrokeColor = "rgba(220,220,220,1)",
                                      PointColor = "rgba(220,220,220,1)",
                                      PointStrokeColor = "#fff",
                                      PointHighlightFill = "#fff",
                                      PointHighlightStroke = "rgba(220,220,220,1)"
             */

            return PartialView(stampcards);
        }

        // GET: SalesStatistics
        public async Task<ActionResult> Sales()
        {
            var currentCompany = await CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString());
            var salesProduct = await ProductRepository.SalesPerProduct(currentCompany); //TODO!

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
            var currentCompany = await CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString());
            var countEnduser = await EnduserRepository.CountEnduser(currentCompany); //TODO!
            return PartialView(countEnduser);
        }
    }

    public class BarChartDTO
    {
        public Product Product { get; set; }
        public BarChart BarChart { get; set; }
    }
}
