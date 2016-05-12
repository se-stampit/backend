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
        public static IEnumerable<Company> CompanyList = new List<Company>
        {
            new Company()
                {
                    Id=Guid.NewGuid().ToString(),
                    CompanyName="Fussal",
                    ContactName="Mr. Fussal",
                    ContactAddress="fussal@gmx.at",
                    Description="MyFrozenyogurt, MyCafé, MyDonuts und MyYogurt"
                },
            new Company()
                {
                    Id=Guid.NewGuid().ToString(),
                    CompanyName="KebapHouse",
                    ContactName="KebapMan",
                    Description="Nice kebap house"
                }
        }.AsReadOnly();

        public static IEnumerable<Store> StoreList = new List<Store>
        {
            new Store()
                {
                    Id=Guid.NewGuid().ToString(),
                    Company=CompanyList.FirstOrDefault(),
                    Latitude=48.303487,
                    Longitude=14.288781,
                    Address="Landstraße 34"
                },
                new Store()
                {
                    Id=Guid.NewGuid().ToString(),
                    Company=CompanyList.FirstOrDefault(),
                    Latitude=48.302487,
                    Longitude=14.285555,
                    Address="Landstraße 56"
                },
        };

        public static IEnumerable<Product> ProductList = new List<Product>
        {
            new Product()
            {
                Company=CompanyList.FirstOrDefault(),
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
                Company=CompanyList.FirstOrDefault(),
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
                Company=CompanyList.LastOrDefault(),
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
                Company=CompanyList.LastOrDefault(),
                Id = Guid.NewGuid().ToString(),
                Productname="Pizza",
                Price=7,
                Active=true,
                BonusDescription="Get one free pizza",
                RequiredStampCount=10,
                MaxDuration=365
            }
        }.AsReadOnly();

        public static IEnumerable<Businessuser> UserList = new List<Businessuser>
        {
            new Businessuser()
                {
                   Role=new Role() {RoleName="Manager"},
                   RoleId="Manager",
                    Company=CompanyList.FirstOrDefault(),
                    FirstName="Mr.",
                    LastName="Fussal",
                    MailAddress="fussal@gmx.at"
                },
                new Businessuser()
                {
                    Role=new Role() {RoleName="Shop"},
                    RoleId="Shop",
                    FirstName="Shop",
                    LastName="",
                    MailAddress="shop@gmx.at",
                    Company=CompanyList.FirstOrDefault()
                },
        };

        // GET: CompanyData
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyData/Products
        public PartialViewResult Products()
        {
            return PartialView(ProductList);
        }
        // GET: CompanyData/CompanyData
        public PartialViewResult CompanyData()
        {
            return PartialView(CompanyList.First());
        }

        // GET: CompanyData/Stores
        public PartialViewResult Stores()
        {
            return PartialView(StoreList);
        }

        // GET: CompanyData/Users
        public PartialViewResult Users()
        {
            return PartialView(UserList);
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

                return RedirectToAction("Products");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyData/Edit/5
        public ActionResult Edit(String id)
        {
            return View(ProductList.FirstOrDefault());
        }

        // POST: CompanyData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(String id, FormCollection collection)
        {
            var product = ProductList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            try
            {
                // TODO: Add update logic here
                //get all fields
                foreach (var key in collection.AllKeys)
                {
                    if (key.ToString().CompareTo("__RequestVerificationToken") != 0 && key.ToString().CompareTo("Id") != 0)
                    {
                        

                    }
                        
                }

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

        public JsonResult GetStores()
        {
            return Json(StoreList, JsonRequestBehavior.AllowGet);
        }
    }
}
