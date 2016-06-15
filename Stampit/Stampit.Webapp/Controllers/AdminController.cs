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
    [Authorize]
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
            return PartialView();
        }

        public async Task<PartialViewResult> AdminUser()
        {
            var userlist = await BusinessuserRepository.GetAllAsync(0);
            return PartialView(userlist.Where(user => user.Role.RoleName.Equals("Admin")));
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
    }
}
