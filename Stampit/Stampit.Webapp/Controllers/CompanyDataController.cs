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
            var company = await CompanyRepository.FindByIdAsync("ID123");
            return PartialView(company);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Company company)
        {
            if(company == null) return RedirectToAction("Index","Profile");

            Company com = await CompanyRepository.FindByIdAsync(company.Id);
            try
            {
                /*
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0) {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    var image = CommonType.ImageUtil.GetImageFromUrl(fileName);
                    company.Blob.Content = image.Result;
                }*/
               
                //persist data
                
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
    }
}
