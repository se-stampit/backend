using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stampit.CommonType;

namespace Stampit.Webapp.Controllers
{
    [StampitAuthorize(Roles = "Manager")]
    public class ProfileController : Controller
    {
        private ICompanyRepository CompanyRepository { get; }
        private IStoreRepository StoreRepository { get; }

        public ProfileController(ICompanyRepository companyRepository, IStoreRepository storeRepository)
        {
            this.CompanyRepository = companyRepository;
            this.StoreRepository = storeRepository;
        }

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: TestView
        public PartialViewResult TestView()
        {
            return PartialView();
        }

        // GET: CompanyData/Stores
        public PartialViewResult Stores()
        {
            var storelist = StoreRepository.GetAllAsync(0).Result;
            return PartialView(storelist);
        }
        // GET: CompanyData/Stores
        public PartialViewResult StoreList()
        {
            var storelist = StoreRepository.GetAllAsync(0).Result;
            return PartialView(storelist);
        }

        // GET: CompanyData/Stores
        public ActionResult MapsView(String id)
        {
            var store = StoreRepository.FindByIdAsync(id).Result;
            return PartialView(store);
        }
    }
}
