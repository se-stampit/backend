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
    public class UsersController : Controller
    {
        private IBusinessuserRepository BusinessuserRepository { get; }

        public UsersController(IBusinessuserRepository businessuserRepository)
        {
            this.BusinessuserRepository = businessuserRepository;
        }

        // GET: Users
        public async Task<PartialViewResult> Index()
        {
            var userlist = await BusinessuserRepository.GetAllAsync(0);
            return PartialView(userlist);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(Businessuser user)
        {
            if (user == null) return View();

            try
            {
                await BusinessuserRepository.CreateOrUpdateAsync(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(String id)
        {
            var product = await BusinessuserRepository.FindByIdAsync(id);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Businessuser item)
        {
            if (item == null) return View();

            try
            {
                await BusinessuserRepository.CreateOrUpdateAsync(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(String id)
        {
            var product = await BusinessuserRepository.FindByIdAsync(id);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Businessuser product)
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
