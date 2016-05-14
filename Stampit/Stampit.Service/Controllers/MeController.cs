using Stampit.CommonType;
using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Stampit.Service.Controllers
{
    public class MeController : ApiController
    {
        private IStampCodeService StampcodeService { get; }
        private IEnduserRepository EnduserRepository { get; }
        private IStampcardRepository StampcardRepository { get; }
        private IPushNotifier Notifier { get; }
        
        public MeController(IStampCodeService stampcodeService, IEnduserRepository enduserRepository, IStampcardRepository stampcardRepository, IPushNotifier notifier)
        {
            this.StampcodeService = stampcodeService;
            this.EnduserRepository = enduserRepository;
            this.StampcardRepository = stampcardRepository;
            this.Notifier = notifier;
        }

        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Scan([FromBody]StampcodeDTO stampcode)
        {
            var scanner = (await EnduserRepository.GetAllAsync(0)).First();
            try
            {
                await StampcodeService.ScanCodeAsync(stampcode.Stampcode, scanner);
                this.Notifier.OnScan(stampcode.Stampcode, true);
                return Ok();
            }
            catch(IllegalCodeException)
            {
                this.Notifier.OnScan(stampcode.Stampcode, false);
                return StatusCode(HttpStatusCode.BadRequest);
            }
            catch(NotRedeemableStampcardException)
            {
                this.Notifier.OnScan(stampcode.Stampcode, false);
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        
        [System.Web.Http.HttpGet]
        [Route("api/me/stampcard")]
        public async Task<IHttpActionResult> GetStampcards()
        {
            var scanner = (await EnduserRepository.GetAllAsync(0)).First();
            var stampcards = await StampcardRepository.GetAllStampcards(scanner, 0);
            var result = from s in stampcards
                         select new
                         {
                             id = s.Id,
                             createdAt = s.CreatedAt,
                             updatedAt = s.UpdatedAt,
                             userId = s.EnduserId,
                             companyId = s.Product.CompanyId,
                             productName = s.Product.Productname,
                             requiredStampCount = s.Product.RequiredStampCount,
                             bonusDescription = s.Product.BonusDescription,
                             maxDuration = s.Product.MaxDuration,
                             isUsed = s.IsRedeemed,
                             currentStampCount = s.Stamps?.Count ?? 0
                         };
            return Content(HttpStatusCode.OK, result);
        }
        
        [System.Web.Http.HttpGet]
        [Route("api/me/stampcard/count")]
        public async Task<IHttpActionResult> GetStampcardCount()
        {
            var scanner = (await EnduserRepository.GetAllAsync(0)).First();
            var stampcards = await StampcardRepository.GetAllStampcards(scanner, 0);
            return Content(HttpStatusCode.OK, new { count = stampcards.Count() });
        }

        public class StampcodeDTO
        {
            [Newtonsoft.Json.JsonProperty("stampcode")]
            public string Stampcode { get; set; }
        }
    }
}
