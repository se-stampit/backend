using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stampit.CommonType;

namespace Stampit.Webapp.Controllers
{
    public class CompanyDataController : Controller
    {
        private ICompanyRepository CompanyRepository { get; }

        public CompanyDataController(ICompanyRepository companyRepository)
        {
            this.CompanyRepository = companyRepository;
        }

        // GET: CompanyData
        public async Task<PartialViewResult> Index()
        {
            Session["companyID"] = "ID123";
            var company = await CompanyRepository.FindByIdAsync("ID123");
            return PartialView(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveData(Company company)
        {
            if (company == null) return RedirectToAction("Index", "Profile");

            try
            {
                //persist data 
                Company com = await CompanyRepository.FindByIdAsync(company.Id);
                com.CompanyName = company.CompanyName;
                com.ContactAddress = company.ContactAddress;
                com.ContactName = company.ContactName;
                com.Description = company.Description;

                await CompanyRepository.CreateOrUpdateAsync(com);
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return RedirectToAction("Index", "Profile");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveImage(HttpPostedFileBase file)
        {
            try
            {
                Company com = await CompanyRepository.FindByIdAsync("ID123");
                
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    com.Blob = new Blob
                    {
                        Id = Guid.NewGuid().ToString().Replace("-", ""),
                        CreatedAt = DateTime.Now,
                        Filename = file.FileName,
                        ContentType = file.ContentType,
                        Content = await CommonType.ImageUtil.GetImageFromStream(file.InputStream)
                };

                    await CompanyRepository.CreateOrUpdateAsync(com);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}
