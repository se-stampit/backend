using Stampit.CommonType;
using Stampit.Entity;
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
    [Authorize]
    public class KioskModeController : Controller
    {
        private const string SESSION_STATE = "SessionState";
        private IProductRepository ProductRepository { get; }
        private IQrCodeGenerator QrCodeGenerator { get; }
        private IStampCodeProvider StampCodeProvider { get; }
        private IStampCodeService StampCodeService { get; }

        private Company currentCompany;

        public KioskModeController(IQrCodeGenerator qrCodeGenerator, IStampCodeProvider stampCodeProvider, IStampCodeService stampCodeService, IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
            this.QrCodeGenerator = qrCodeGenerator;
            this.StampCodeService = stampCodeService;
            this.StampCodeProvider = stampCodeProvider;
        }

        public async Task<ActionResult> Index()
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null)
            {
                this.currentCompany = (await ProductRepository.GetAllAsync(0)).First().Company;
                sessionState = new SessionState { Model = new List<ProductViewModel>() };
                foreach (var product in await ProductRepository.FindProductsFromCompany(this.currentCompany, 0))
                {
                    sessionState.Model.Add(new ProductViewModel { Product = product, Count = 0, IsSelected = !sessionState.Model.Any() });
                }
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
            
            var stampcode = StampCodeProvider.GenerateStampCode((Dictionary<Product,int>)sessionState);
            var img = await ImageUtil.GetImageFromUrl(QrCodeGenerator.GetQrCodeUrl(stampcode));
            this.StampCodeService.AddStampcode(stampcode, (Dictionary<Product, int>)sessionState);

            return View(new ScanViewModel
            {
                Base64Img = Convert.ToBase64String(img),
                Stampcode = stampcode
            });
        }

        public async Task<ActionResult> RedeemStampCard()
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.Model == null) return RedirectToAction("Index");

            var stampcode = StampCodeProvider.GenerateRedeemCode(sessionState.SelectedViewModel.Product);
            var img = await ImageUtil.GetImageFromUrl(QrCodeGenerator.GetQrCodeUrl(stampcode));
            var products = sessionState.Model;
            this.StampCodeService.AddReedemtionStampcode(stampcode, sessionState.SelectedViewModel.Product);

            return View(new ScanViewModel
            {
                Base64Img = Convert.ToBase64String(img),
                Stampcode = stampcode,
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
        public IList<ProductViewModel> Model { get; set; }
        public ProductViewModel SelectedViewModel { get; set; }

        public static explicit operator Dictionary<Product,int>(SessionState session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            return session.Model?.ToDictionary(vm => vm.Product, vm => vm.Count);
        }
    }
}
