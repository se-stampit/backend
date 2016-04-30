using Stampit.CommonType;
using Stampit.Logic.Interface;
using Stampit.Webapp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    public class KioskModeController : Controller
    {
        private const string SESSION_STATE = "SessionState";

        private IQrCodeGenerator QrCodeGenerator { get; }

        public KioskModeController(IQrCodeGenerator qrCodeGenerator)
        {
            this.QrCodeGenerator = qrCodeGenerator;
        }

        public ActionResult Index()
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null)
            {
                sessionState = new SessionState
                {
                    Model = new List<ProductViewModel>
                    {
                        new ProductViewModel { Name = "Pizza", Count = 0 },
                        new ProductViewModel { Name = "Kebab", Count = 0 }
                    },
                    SelectedViewModel = null
                };
                Session[SESSION_STATE] = sessionState;
            }

            return View(sessionState.Model);
        }

        public ActionResult SetCount(int count)
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.SelectedViewModel == null) return RedirectToAction("Index");

            sessionState.SelectedViewModel.Count = count;
            Session[SESSION_STATE] = sessionState;

            return View("Index", sessionState.Model);
        }

        public ActionResult SelectProductViewModel(string selectedProduct)
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.Model == null) return RedirectToAction("Index");

            sessionState.SelectedViewModel = sessionState.Model.Where(vm => vm.Name == selectedProduct).FirstOrDefault();
            Session[SESSION_STATE] = sessionState;
            return View("Index", sessionState.Model);
        }

        public async Task<ActionResult> ShowStampGenerationQrCode()
        {
            var img = await ImageUtil.GetImageFromUrl(QrCodeGenerator.GetQrCodeUrl("testtoken"));
            return View(Convert.ToBase64String(img) as object);
        }

        public ActionResult Clear()
        {
            Session[SESSION_STATE] = null;
            return RedirectToAction("Index");
        }
    }

    public class SessionState
    {
        public IEnumerable<ProductViewModel> Model { get; set; }
        public ProductViewModel SelectedViewModel { get; set; }
    }
}