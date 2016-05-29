using Stampit.Entity;
using Stampit.Logic.Interface;
using Stampit.Webapp.Models;
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
        private IRoleRepository RoleRepository { get; }
        private static IEnumerable<Role> Roles { get; set; }

        public UsersController(IBusinessuserRepository businessuserRepository, IRoleRepository roleRepository)
        {
            this.BusinessuserRepository = businessuserRepository;
            this.RoleRepository = roleRepository;
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
            var user = await GetUsersViewModel(id);
            return View(user);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UsersViewModel item)
        {
            if (item == null) return View();

            try
            {
                item.User.Role = await RoleRepository.FindByIdAsync(item.User.RoleId);
                await BusinessuserRepository.CreateOrUpdateAsync(item.User);

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
            var product = await BusinessuserRepository.FindByIdAsync(id);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Businessuser user)
        {
            try
            {
                await BusinessuserRepository.Delete(user);
                return RedirectToAction("Index", "CompanyData");
            }
            catch
            {
                return View();
            }
        }

        private async Task<UsersViewModel> GetUsersViewModel(String id)
        {
            UsersViewModel viewModel = new UsersViewModel();

            if (Roles == null)
            {
                Roles = await RoleRepository.GetAllAsync(0);
            }

            var user = await BusinessuserRepository.FindByIdAsync(id);
            viewModel.User = user;

            viewModel.Roles = Roles.Select(r => new SelectListItem
            {
                Text = r.RoleName,
                Value = r.Id
            });

            return viewModel;
        }
    }
}
