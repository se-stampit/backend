using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    public class StoresController : Controller
    {
        private IStoreRepository StoreRepository { get; }
    
        public StoresController(IStoreRepository storeRepository)
        {
            this.StoreRepository = storeRepository;
        }

        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stores/Create
        public ActionResult StoresMap()
        {
            return PartialView();
        }
        // GET: Stores/Create
        public async Task<PartialViewResult> StoresList()
        {
            var storelist = await StoreRepository.GetAllAsync(0);
            return PartialView(storelist);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
