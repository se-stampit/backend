using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    public class CompanyDataController : Controller
    {
        // GET: CompanyData
        public ActionResult Index()
        {
            return View(new List<Company>
            {
                new Company { CompanyName = "HANS" },
                new Company { CompanyName = "PETER" }
            });
        }

        // GET: CompanyData/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyData/Create
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

        // GET: CompanyData/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompanyData/Edit/5
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

        // GET: CompanyData/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyData/Delete/5
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
