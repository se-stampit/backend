using Stampit.CommonType;
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
    [StampitAuthorize(Roles = "Manager")]
    public class UsersController : Controller
    {
        private IBusinessuserRepository BusinessuserRepository { get; }
        private IRoleRepository RoleRepository { get; }
        private ICompanyRepository CompanyRepository { get; }

        private static IEnumerable<Role> Roles { get; set; }

        public UsersController(IBusinessuserRepository businessuserRepository, IRoleRepository roleRepository, ICompanyRepository companyRepository)
        {
            this.BusinessuserRepository = businessuserRepository;
            this.RoleRepository = roleRepository;
            this.CompanyRepository = companyRepository;
        }

        // GET: Users
        public PartialViewResult Index()
        {
            string companyID = Session[Setting.SESSION_COMPANY].ToString();
            var userlist = BusinessuserRepository.GetAllAsync(0).Result;
            return PartialView(userlist.Where(user => user.CompanyId == companyID));
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var user = await GetUsersViewModel("");
            return View(user);
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(UsersViewModel item)
        {
            if (item == null) return View();

            try
            {
                string companyID = Session[Setting.SESSION_COMPANY].ToString();
                item.User.CompanyId = companyID;

                await BusinessuserRepository.CreateOrUpdateAsync(item.User);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(string id)
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

            //TODO: update attributes

            try
            {
                var user = await BusinessuserRepository.FindByIdAsync(item.User.Id);
                user.Id = item.User.Id;
                user.RoleId = item.User.RoleId;
                user.FirstName = item.User.FirstName;
                user.LastName = item.User.LastName;
                user.MailAddress = item.User.MailAddress;

                await BusinessuserRepository.CreateOrUpdateAsync(user);

                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }
        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await BusinessuserRepository.FindByIdAsync(id);
            return View(user);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Businessuser user)
        {
            try
            {
                await BusinessuserRepository.Delete(user);
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View();
            }
        }

        private async Task<UsersViewModel> GetUsersViewModel(string id)
        {
            UsersViewModel viewModel = new UsersViewModel();

            if (Roles == null) {
                Roles = await RoleRepository.GetAllAsync(0);
                Roles = Roles.Where(role => !role.RoleName.Equals("Admin"));
            }

            if(id!=null && !id.Equals("")) {
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
