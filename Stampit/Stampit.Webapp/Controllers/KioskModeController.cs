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
    [StampitAuthorize(Roles = "KioskUser,Manager")]
    public class KioskModeController : Controller
    {
        private IProductRepository ProductRepository { get; }
        private ICompanyRepository CompanyRepository { get; }
        private IQrCodeGenerator QrCodeGenerator { get; }
        private IStampCodeProvider StampCodeProvider { get; }
        private IStampCodeService StampCodeService { get; }

        private Company currentCompany;

        public KioskModeController(IQrCodeGenerator qrCodeGenerator, IStampCodeProvider stampCodeProvider, IStampCodeService stampCodeService, IProductRepository productRepository, ICompanyRepository companyRepository)
        {
            this.ProductRepository = productRepository;
            this.CompanyRepository = companyRepository;
            this.QrCodeGenerator = qrCodeGenerator;
            this.StampCodeService = stampCodeService;
            this.StampCodeProvider = stampCodeProvider;
        }

        public async Task<ActionResult> Index()
        {
            var sessionState = Session[Setting.SESSION_PRODUCTS] as SessionState;
            var comId = Session[Setting.SESSION_COMPANY].ToString();
            if (sessionState == null)
            {
                this.currentCompany = (await CompanyRepository.FindByIdAsync(comId));

                sessionState = new SessionState { Model = new List<ProductViewModel>() };
                foreach (var product in await ProductRepository.FindProductsFromCompany(this.currentCompany, 0))
                {
                    sessionState.Model.Add(new ProductViewModel { Product = product, Count = 0, IsSelected = !sessionState.Model.Any() });
                }
                sessionState.SelectedViewModel = sessionState.Model.FirstOrDefault();
                Session[Setting.SESSION_PRODUCTS] = sessionState;
            }

            return View(sessionState.Model);
        }

        public ActionResult SetCount(int count)
        {
            var sessionState = Session[Setting.SESSION_PRODUCTS] as SessionState;
            if (sessionState == null || sessionState.SelectedViewModel == null) return RedirectToAction("Index");

            sessionState.SelectedViewModel.Count = count;
            Session[Setting.SESSION_PRODUCTS] = sessionState;

            return View("Index", sessionState.Model);
        }

        public ActionResult SelectProductViewModel(string selectedProduct)
        {
            try
            {
                SelectProduct(selectedProduct);
                return View("Index", (Session[Setting.SESSION_PRODUCTS] as SessionState)?.Model);
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

            var sessionState = Session[Setting.SESSION_PRODUCTS] as SessionState;
            if (sessionState == null || sessionState.Model == null)
                throw new InvalidOperationException("The sessionstate does not contain a model and can theirfore not select any product");

            if (sessionState.SelectedViewModel != null)
                sessionState.SelectedViewModel.IsSelected = false;
            sessionState.SelectedViewModel = sessionState.Model.Where(vm => vm?.Product?.Productname == selectedProduct).FirstOrDefault();
            sessionState.SelectedViewModel.IsSelected = true;
            Session[Setting.SESSION_PRODUCTS] = sessionState;
        }

        public async Task<ActionResult> ShowStampGenerationQrCode()
        {
            var sessionState = Session[Setting.SESSION_PRODUCTS] as SessionState;
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
            var sessionState = Session[Setting.SESSION_PRODUCTS] as SessionState;
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
            Session[Setting.SESSION_PRODUCTS] = null;
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
