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

        public ProductsController(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }
        // GET: Products
        public async Task<PartialViewResult> Index(String id)
        {
            //TODO: get all from company
            var productlist = await ProductRepository.GetAllAsync(0);
            return PartialView(productlist);
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            if (product == null) return View();

            try
            {
                await ProductRepository.CreateOrUpdateAsync(product);

                return RedirectToAction("Index", "CompanyData");
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

            try
            {
                await ProductRepository.CreateOrUpdateAsync(product);

                return RedirectToAction("Index", "CompanyData");
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
            await ProductRepository.Delete(product);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Product product)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", "CompanyData");
            }
            catch
            {
                return View();
            }
        }
    }
}
