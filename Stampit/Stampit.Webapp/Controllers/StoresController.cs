using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private IStoreRepository StoreRepository { get; }
        private ICompanyRepository CompanyRepository { get; }

        public StoresController(IStoreRepository storeRepository, ICompanyRepository companyRepository)
        {
            this.StoreRepository = storeRepository;
            this.CompanyRepository = companyRepository;
        }

        // GET: Stores
        public ActionResult Index()
        {
            return PartialView();
        }

        public async Task<ActionResult> StoresMap()
        {
            String companyID = Session["companyID"].ToString();
            var storelist = await StoreRepository.GetAllAsync(0);
            return PartialView(storelist.Where(com => com.CompanyId == companyID));
        }

        public async Task<PartialViewResult> StoresList()
        {
            String companyID = Session["companyID"].ToString();
            var storelist = await StoreRepository.GetAllAsync(0);
            return PartialView(storelist.Where(com => com.CompanyId == companyID));
        }
        
        // GET: Stores/Create
        public ActionResult Create(double? lat=null, double? lng=null)
        {
            if(lat==null||lng==null) return RedirectToAction("Index", "Profile");

            Store s = new Store();
            s.Latitude = lat.Value;
            s.Longitude = lng.Value;

            return View(s);
        }

        // POST: Stores/Create
        [HttpPost]
        public async Task<ActionResult> Create(Store store)
        {
            if (store == null) return View();

            try
            {
                String companyID = Session["companyID"].ToString();
                var company = await CompanyRepository.FindByIdAsync(companyID);

                store.CompanyId = companyID;
                store.Company = company;
                await StoreRepository.CreateOrUpdateAsync(store);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(String id)
        {
            var store = await StoreRepository.FindByIdAsync(id);
            return View(store);
        }

        // POST: Stores/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Store store)
        {
            if (store == null) return View();
            var sto = await StoreRepository.FindByIdAsync(store.Id);

            try
            {
                sto.Address = store.Address;
                sto.Description = store.Description;
                sto.Latitude = store.Latitude;
                sto.Longitude = store.Longitude;
                sto.StoreName = store.StoreName;

                await StoreRepository.CreateOrUpdateAsync(store);
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stores/Delete/5
        public async Task<ActionResult> Delete(String id)
        {
            var store = await StoreRepository.FindByIdAsync(id);
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Store store)
        {
            try
            {
                await StoreRepository.Delete(store);
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
