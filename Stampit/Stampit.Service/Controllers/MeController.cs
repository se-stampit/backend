using Newtonsoft.Json;
using Stampit.CommonType;
using Stampit.Logic.Interface;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Stampit.Service.Controllers
{
    public class MeController : ApiController
    {
        private IStampCodeService StampcodeService { get; }
        private IEnduserRepository EnduserRepository { get; }
        private IStampcardRepository StampcardRepository { get; }
        private IBlobRepository BlobRepository { get; }
        private IPushNotifier Notifier { get; }
        
        public MeController(IStampCodeService stampcodeService, IEnduserRepository enduserRepository, IStampcardRepository stampcardRepository, IBlobRepository blobRepository, IPushNotifier notifier)
        {
            this.StampcodeService = stampcodeService;
            this.EnduserRepository = enduserRepository;
            this.StampcardRepository = stampcardRepository;
            this.BlobRepository = blobRepository;
            this.Notifier = notifier;
        }

        [HttpGet]
        [Route("api/me")]
        public async Task<IHttpActionResult> GetMe()
        {
            var mail = Request.GetOwinContext().Environment[Setting.AUTH_ENVIRONMENT_ID]?.ToString();
            var user = await EnduserRepository.FindByMailAddress(mail);
            if (user == null) return BadRequest("No user is currently logged in and can't be returned");

            return Content(HttpStatusCode.OK,
                new
                {
                    id = user.Id,
                    createdAt = user.CreatedAt,
                    updatedAt = user.UpdatedAt,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    mailAddress = user.MailAddress
                }
            );
        }

        [HttpPost]
        [Route("api/me/scan")]
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
        
        [HttpGet]
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
        
        [HttpGet]
        [Route("api/me/stampcard/count")]
        public async Task<IHttpActionResult> GetStampcardCount()
        {
            var scanner = (await EnduserRepository.GetAllAsync(0)).First();
            var stampcards = await StampcardRepository.GetAllStampcards(scanner, 0);
            return Content(HttpStatusCode.OK, new { count = stampcards.Count() });
        }

        public class StampcodeDTO
        {
            [JsonProperty("stampcode")]
            public string Stampcode { get; set; }
        }
    }
}
