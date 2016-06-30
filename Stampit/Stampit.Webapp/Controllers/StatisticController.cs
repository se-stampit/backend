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
        public ActionResult Index()
        {
            return View();
        }

        // GET: StampCardStatistics
        public ActionResult CardsInCirculation()
        {
            var currentCompany = CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString()).Result;
            var total = StampcardRepository.CountStampcardsFromCompany(currentCompany).Result;
            var redeemed = StampcardRepository.CountRedeemedStampcardsFromCompany(currentCompany).Result;
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
        public ActionResult CardStatus()
        {
            var currentCompany = CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString()).Result;
            var stampcardsCompany = StampcardRepository.GetAllStampcardsFromCompany(currentCompany).Result;

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
        public ActionResult Sales()
        {
            var currentCompany = CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString()).Result;
            var salesProduct = ProductRepository.SalesPerProduct(currentCompany).Result;

            Tupel<string,string> randColor;

            var sales = from p in salesProduct
                    select new SimpleData {
                        Value = p.Value,
                        Color = (randColor = RandomColor()).Arg1,
                        Highlight = randColor.Arg2,
                        Label = p.Key.Productname
                    };
            
            return PartialView(sales);
        }

        // GET: CustomerStatistics
        public ActionResult Customer()
        {
            var currentCompany = CompanyRepository.FindByIdAsync(Session[Setting.SESSION_COMPANY].ToString()).Result;
            var countEnduser = EnduserRepository.CountEnduser(currentCompany).Result;
            return PartialView(countEnduser);
        }

        private static Random rand = new Random(System.Environment.TickCount);

        private Tupel<string,string> RandomColor()
        {
            string[] hex = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"};

            string result = "#";
            string highlightResult = "#";
            for (int i = 0; i < 6; i++)
            {
                int next = rand.Next(0, hex.Length - 1);
                result += hex[next];
                highlightResult += hex[next + 2 > 15 ? 15 : next + 2];
            }

            return new Tupel<string, string>(result,highlightResult);
        }
    }

    public class BarChartDTO
    {
        public Product Product { get; set; }
        public BarChart BarChart { get; set; }
    }    
}
