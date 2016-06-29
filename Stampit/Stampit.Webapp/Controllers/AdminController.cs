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
using System.Web.Security;

namespace Stampit.Webapp.Controllers
{
    [StampitAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IBusinessuserRepository BusinessuserRepository { get; }
        private IRoleRepository RoleRepository { get; }
        private ICompanyRepository CompanyRepository { get; }

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
            return View();
        }

        // GET: Admin/Create
        public ActionResult SelectCompany()
        {
            var model = getModelView().Result;
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectCompany(AdminCompanySelectorViewModel item) {
            var model = getModelView().Result;

            if (item == null) return PartialView(model);

            Session[Setting.SESSION_COMPANY] = item.Company.Id;
            Session[Setting.SESSION_PRODUCTS] = null;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(AdminCompanyViewModel item)
        {
            if (item == null) return View();

            item.Company.Products = new List<Product>();
            item.Company.Stores = new List<Store>();
            item.Company.Businessusers = new List<Businessuser>();

            //save company
            await CompanyRepository.CreateOrUpdateAsync(item.Company);

            var companies = await CompanyRepository.GetAllAsync(0);
            var company = companies.Where(com => com.CompanyName.Equals(item.Company.CompanyName)).First();

            //save user
            var roles = await RoleRepository.GetAllAsync(0);
            var role = roles.Where(ro => ro.RoleName.Equals("Manager")).First();

            item.User.Role = role;
            item.User.RoleId = role.Id;
            item.User.Company = company;
            item.User.CompanyId = company.Id;

            await BusinessuserRepository.CreateOrUpdateAsync(item.User);

            return View();
        }
        public async Task<ActionResult> AdminUser()
        {
            var userlist = await BusinessuserRepository.GetAllAsync(0);
            return View(userlist.Where(user => user.Role.RoleName.Equals("Admin")));
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id != null && !id.Equals(""))
            {
                var user = await BusinessuserRepository.FindByIdAsync(id);
                return View(user);
            }
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Businessuser item)
        {
            if (item == null) return View();
            
            try
            {
                var user = await BusinessuserRepository.FindByIdAsync(item.Id);
                var Roles = await RoleRepository.GetAllAsync(0);
                Roles = Roles.Where(role => role.RoleName.Equals("Admin"));

                user.Role = Roles.FirstOrDefault();
                user.RoleId = user.Role.Id;
                user.FirstName = item.FirstName;
                user.LastName = item.LastName;
                user.MailAddress = item.MailAddress;

                await BusinessuserRepository.CreateOrUpdateAsync(user);

                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UsersViewModel item)
        {
            if (item == null) return View();

            try
            {
                var Roles = await RoleRepository.GetAllAsync(0);
                Roles = Roles.Where(role => role.RoleName.Equals("Admin"));
                item.User.Role = Roles.FirstOrDefault();

                await BusinessuserRepository.CreateOrUpdateAsync(item.User);

                return RedirectToAction("AdminUser");
            }
            catch
            {
                return RedirectToAction("AdminUser");
            }
        }
        
        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id != null && !id.Equals(""))
            {
                var user = await BusinessuserRepository.FindByIdAsync(id);
                return View(user);
            }
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Businessuser user)
        {
            try
            {
                await BusinessuserRepository.Delete(user);
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }

        private async Task<AdminCompanySelectorViewModel> getModelView()
        {
            var companies = await CompanyRepository.GetAllAsync(0);
            AdminCompanySelectorViewModel model = new AdminCompanySelectorViewModel();
            model.Companies = companies.Select(r => new SelectListItem
            {
                Text = r.CompanyName,
                Value = r.Id
            });
            model.Company = new Company();

            return model;
        }
    }
}
