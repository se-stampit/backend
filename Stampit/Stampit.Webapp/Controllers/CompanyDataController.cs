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
    public class CompanyDataController : Controller
    {
        private ICompanyRepository CompanyRepository { get; }
        private IStoreRepository StoreRepository { get; }
        private IProductRepository ProductRepository { get; }
        private IBusinessuserRepository BusinessuserRepository { get; }

        public CompanyDataController(ICompanyRepository companyRepository, IStoreRepository storeRepository, IProductRepository productRepository, IBusinessuserRepository businessuserRepository)
        {
            this.CompanyRepository = companyRepository;
            this.StoreRepository = storeRepository;
            this.ProductRepository = productRepository;
            this.BusinessuserRepository = businessuserRepository;
        }

        // GET: CompanyData
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyData
        public PartialViewResult TestView()
        {
            return PartialView();
        }

        // GET: CompanyData/CompanyData
        public async Task<PartialViewResult> CompanyDetail()
        {
            var companylist = await CompanyRepository.GetAllAsync(0);
            return PartialView(companylist.FirstOrDefault());
        }

        // GET: CompanyData/Stores
        public async Task<PartialViewResult> Stores()
        {
            var storelist = await StoreRepository.GetAllAsync(0);
            return PartialView(storelist);
        }

        // GET: CompanyData/Stores
        public async Task<ActionResult> MapsEdit(String id)
        {
            var store = await StoreRepository.FindByIdAsync(id);
            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Company company, HttpPostedFileBase file)
        {
            if(company == null) return RedirectToAction("Index","CompanyData");
            
            try
            {
                await CompanyRepository.CreateOrUpdateAsync(company);
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);

                    var image = CommonType.ImageUtil.GetImageFromUrl(fileName);

                    company.Blob.Content = image.Result;
                    await CompanyRepository.CreateOrUpdateAsync(company);
                }
                // redirect back to the index action to show the form once again
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
