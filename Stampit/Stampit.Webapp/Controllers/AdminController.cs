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
    public class AdminController : Controller
    {
        private IBusinessuserRepository BusinessuserRepository { get; }
        private IRoleRepository RoleRepository { get; }
        private ICompanyRepository CompanyRepository { get; }

        private static IEnumerable<Role> Roles { get; set; }

        public AdminController(IBusinessuserRepository businessuserRepository, IRoleRepository roleRepository, ICompanyRepository companyRepository)
        {
            this.BusinessuserRepository = businessuserRepository;
            this.RoleRepository = roleRepository;
            this.CompanyRepository = companyRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult CreateCompany()
        {
            return PartialView();
        }

        public async Task<PartialViewResult> AdminUser()
        {
            var userlist = await BusinessuserRepository.GetAllAsync(0);
            return PartialView(userlist.Where(user => user.Role.RoleName.Equals("Admin")));
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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

        private async Task<UsersViewModel> GetUsersViewModel(String id)
        {
            UsersViewModel viewModel = new UsersViewModel();

            if (Roles == null)
            {
                Roles = await RoleRepository.GetAllAsync(0);
                Roles = Roles.Where(role => role.RoleName.Equals("Admin"));
            }

            if (id != null && !id.Equals(""))
            {
                var user = await BusinessuserRepository.FindByIdAsync(id);
                viewModel.User = user;
            }

            viewModel.Roles = Roles.Select(r => new SelectListItem
            {
                Text = r.RoleName,
                Value = r.Id
            });

            return viewModel;
        }
    }
}
