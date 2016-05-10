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
        public static IEnumerable<Company> CompaniesList = new List<Company>
        {
            new Company()
                {
                    Id=Guid.NewGuid().ToString(),
                    CompanyName="CoffeeRoom",
                    ContactName="CoffeeMaster",
                    Description="Nice coffee house"
                },
            new Company()
                {
                    Id=Guid.NewGuid().ToString(),
                    CompanyName="KebapHouse",
                    ContactName="KebapMan",
                    Description="Nice kebap house"
                }
        }.AsReadOnly();

        public static IEnumerable<Product> ProductsList = new List<Product>
        {
            new Product()
            {
                Company=CompaniesList.FirstOrDefault(),
                Id=Guid.NewGuid().ToString(),
                Productname="Coffee",
                Price=2.5,
                Active=true,
                BonusDescription="Get one free coffee",
                RequiredStampCount=10,
                MaxDuration=365
            },
            new Product()
            {
                Company=CompaniesList.FirstOrDefault(),
                Id =Guid.NewGuid().ToString(),
                Productname="Tea",
                Price=2,
                Active=true,
                BonusDescription="Get one free tea",
                RequiredStampCount=5,
                MaxDuration=365
            },
            new Product()
            {
                Company=CompaniesList.LastOrDefault(),
                Id =Guid.NewGuid().ToString(),
                Productname="Kebap",
                Price=5,
                Active=true,
                BonusDescription="Get one free kebap",
                RequiredStampCount=10,
                MaxDuration=365
            },
                        new Product()
            {
                Company=CompaniesList.LastOrDefault(),
                Id =Guid.NewGuid().ToString(),
                Productname="Pizza",
                Price=7,
                Active=true,
                BonusDescription="Get one free pizza",
                RequiredStampCount=10,
                MaxDuration=365
            }
        }.AsReadOnly();

        // GET: CompanyData
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyData
        public ActionResult Products()
        {
            return PartialView(ProductsList);
        }
        public ActionResult CompanyData()
        {
            return PartialView(CompaniesList.First());
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
        public ActionResult Edit(String id)
        {
            return View(ProductsList.FirstOrDefault());
        }

        // POST: CompanyData/Edit/5
        [HttpPost]
        public ActionResult Edit(String id, FormCollection collection)
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
        public ActionResult Delete(String id)
        {
            return View();
        }

        // POST: CompanyData/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
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
