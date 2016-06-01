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
    public class ProductsController : Controller
    {
        private IProductRepository ProductRepository { get; }
        private ICompanyRepository CompanyRepository { get; }

        public ProductsController(IProductRepository productRepository, ICompanyRepository companyRepository)
        {
            this.ProductRepository = productRepository;
            this.CompanyRepository = companyRepository;
        }
        // GET: Products
        public async Task<PartialViewResult> Index()
        {
            String companyID = Session["companyID"].ToString();
            var productlist = await ProductRepository.GetAllAsync(0);
            return PartialView(productlist.Where(prod => prod.CompanyId == companyID));
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (product == null) return View();

            try
            {
                String companyID = Session["companyID"].ToString();
                var company = await CompanyRepository.FindByIdAsync(companyID);

                product.CompanyId = companyID;
                product.Company = company;
                await ProductRepository.CreateOrUpdateAsync(product);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(String id)
        {
            var product = await ProductRepository.FindByIdAsync(id);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            if (product == null) return View();
            var prod = await ProductRepository.FindByIdAsync(product.Id);

            try
            {
                prod.Active = product.Active;
                prod.BonusDescription = product.BonusDescription;
                prod.MaxDuration = product.MaxDuration;
                prod.Price = product.Price;
                prod.Productname = product.Productname;
                prod.RequiredStampCount = product.RequiredStampCount;

                await ProductRepository.CreateOrUpdateAsync(prod);
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(String id)
        {
            var product = await ProductRepository.FindByIdAsync(id);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Product product)
        {
            try
            {
                await ProductRepository.Delete(product);
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }
    }
}
