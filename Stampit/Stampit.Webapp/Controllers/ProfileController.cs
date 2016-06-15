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
    [Authorize]
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
        public async Task<PartialViewResult> Stores()
        {
            var storelist = await StoreRepository.GetAllAsync(0);
            return PartialView(storelist);
        }
        // GET: CompanyData/Stores
        public async Task<PartialViewResult> StoreList()
        {
            var storelist = await StoreRepository.GetAllAsync(0);
            return PartialView(storelist);
        }

        // GET: CompanyData/Stores
        public async Task<ActionResult> MapsView(String id)
        {
            var store = await StoreRepository.FindByIdAsync(id);
            return PartialView(store);
        }
    }
}
