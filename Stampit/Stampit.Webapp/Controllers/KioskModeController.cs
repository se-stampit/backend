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
        private IProductRepository ProductRepository { get; }
        private IQrCodeGenerator QrCodeGenerator { get; }
        private IStampCodeProvider StampCodeService { get; }

        public KioskModeController(IQrCodeGenerator qrCodeGenerator, IStampCodeProvider stampCodeService, IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
            this.QrCodeGenerator = qrCodeGenerator;
            this.StampCodeService = stampCodeService;
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
                        new ProductViewModel { Product = new Entity.Product { Productname = "Pizza" }, Count = 0, IsSelected = true },
                        new ProductViewModel { Product = new Entity.Product { Productname = "Kebab" }, Count = 0, IsSelected = false }
                    }
                };
                sessionState.SelectedViewModel = sessionState.Model.FirstOrDefault();
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
            try
            {
                SelectProduct(selectedProduct);
                return View("Index", (Session[SESSION_STATE] as SessionState)?.Model);
            }
            catch(InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult SelectProductViewModelRedemtion(string selectedProduct)
        {
            if (string.IsNullOrEmpty(selectedProduct))
                throw new ArgumentNullException(nameof(selectedProduct));

            try
            {
                SelectProduct(selectedProduct);
                return RedirectToAction("RedeemStampCard");
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }

        private void SelectProduct(string selectedProduct)
        {
            if (string.IsNullOrEmpty(selectedProduct))
                throw new ArgumentNullException(nameof(selectedProduct));

            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.Model == null)
                throw new InvalidOperationException("The sessionstate does not contain a model and can theirfore not select any product");

            if (sessionState.SelectedViewModel != null)
                sessionState.SelectedViewModel.IsSelected = false;
            sessionState.SelectedViewModel = sessionState.Model.Where(vm => vm?.Product?.Productname == selectedProduct).FirstOrDefault();
            sessionState.SelectedViewModel.IsSelected = true;
            Session[SESSION_STATE] = sessionState;
        }

        public async Task<ActionResult> ShowStampGenerationQrCode()
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.Model == null) return RedirectToAction("Index");
            
            var stampcode = StampCodeService.GenerateStampCode((Dictionary<Product,int>)sessionState);
            var img = await ImageUtil.GetImageFromUrl(QrCodeGenerator.GetQrCodeUrl(stampcode));
            return View(Convert.ToBase64String(img) as object);
        }

        public async Task<ActionResult> RedeemStampCard()
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.Model == null) return RedirectToAction("Index");

            var img = await ImageUtil.GetImageFromUrl(QrCodeGenerator.GetQrCodeUrl(StampCodeService.GenerateRedeemCode(sessionState.SelectedViewModel.Product)));
            var imgStr = Convert.ToBase64String(img);
            var products = sessionState.Model;

            return View(new RedemtionViewModel()
            {
                Base64Img = imgStr,
                Products = products
            });
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

        public static explicit operator Dictionary<Product,int>(SessionState session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            return session.Model.ToDictionary(vm => vm.Product, vm => vm.Count);
        }
    }
}
