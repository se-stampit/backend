using Newtonsoft.Json;
using Stampit.Entity;
using Stampit.CommonType;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

namespace Stampit.Service.Controllers
{
    public class PublicController : ApiController
    {
        private IBlobRepository BlobRepository { get; }
        private IEnduserRepository EnduserRepository { get; }
        private ICompanyRepository CompanyRepository { get; }
        private IStoreRepository StoreRepository { get; }

        public PublicController(IBlobRepository blobRepository, IEnduserRepository enduserRepository, ICompanyRepository companyRepository, IStoreRepository storeRepository)
        {
            this.BlobRepository = blobRepository;
            this.EnduserRepository = enduserRepository;
            this.CompanyRepository = companyRepository;
            this.StoreRepository = storeRepository;
        }

        [HttpPut]
        [Route("api/login")]
        public async Task<IHttpActionResult> Login([FromBody]Loginprovider loginprovider)
        {
            var context = Request.GetOwinContext();
            var mailaddress = context.Request.Environment[Setting.AUTH_ENVIRONMENT_ID];
            var sessiontoken = context.Request.Environment[Setting.AUTH_ENVIRONMENT_SESSIONTOKEN];
            Enduser enduser = null;

            if (mailaddress != null)
                enduser = await EnduserRepository.FindByMailAddress(mailaddress.ToString());

            if (enduser == null)
                return BadRequest("The given user is not registered");

            return Content(HttpStatusCode.OK, 
                new
                {
                    sessionToken = sessiontoken
                }
            );
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IHttpActionResult> Register([FromBody]Loginprovider loginprovider)
        {
            var context = Request.GetOwinContext();
            Enduser user = await EnduserRepository.FindByMailAddress(loginprovider.Enduser.MailAddress);
            if(user == null)
                await EnduserRepository.CreateOrUpdateAsync(loginprovider.Enduser);

            return Content(HttpStatusCode.OK, 
                new
                {
                    sessionToken = context.Request.Environment[Setting.AUTH_ENVIRONMENT_SESSIONTOKEN]
                }
            );
        }

        [HttpGet]
        [Route("api/stampitprovider")]
        public async Task<IHttpActionResult> GetStampitproviders(int? page = null, int? pagesize = null)
        {
            var companies = await CompanyRepository.GetAllAsync(page ?? 0, pagesize ?? Setting.DEFAULT_PAGE_SIZE);

            return Content(HttpStatusCode.OK, 
                from c in companies
                select new
                {
                    id = c.Id,
                    createdAt = c.CreatedAt,
                    updatedAt = c.UpdatedAt,
                    blobId = c.BlobId,
                    companyName = c.CompanyName,
                    companyAddress = c.ContactAddress,
                    contactName = c.ContactName,
                    description = c.Description
                }
            );
        }

        [HttpGet]
        [Route("api/stampitprovider/count")]
        public async Task<IHttpActionResult> GetStampitproviderCount()
        {
            return Content(HttpStatusCode.OK, 
                new
                {
                    count = await CompanyRepository.Count()
                }
            );
        }

        [HttpGet]
        [Route("api/stampitprovider/{companyid}/stores")]
        public async Task<IHttpActionResult> GetCompanyStores(string companyid, int? page = null, int? pagesize = null)
        {
            var company = await CompanyRepository.FindByIdAsync(companyid);
            if (company == null) return BadRequest("The given companyid in the URL is invalid or does not exist");
            var stores = await StoreRepository.GetStoresOfCompany(company);

            return Content(HttpStatusCode.OK, 
                from s in stores
                select new
                {
                    id = s.Id,
                    createdAt = s.CreatedAt,
                    updatedAt = s.UpdatedAt,
                    companyId = companyid,
                    address = s.Address,
                    latitude = s.Latitude,
                    longitude = s.Longitude
                }
            );
        }

        [HttpGet]
        [Route("api/stampitprovider/{companyid}/stores/count")]
        public async Task<IHttpActionResult> GetCompanyStoreCount(string companyid)
        {
            var company = await CompanyRepository.FindByIdAsync(companyid);
            if (company == null) return BadRequest("The given companyid in the URL is invalid or does not exist");

            return Content(HttpStatusCode.OK,
                new
                {
                    count = await StoreRepository.GetStoresOfCompanyCount(company)
                }
            );
        }

        [HttpGet]
        [Route("api/blob/{blobid}/content")]
        public async Task<HttpResponseMessage> GetBlobContent(string blobid)
        {
            try
            {
                var blob = await BlobRepository.FindByIdAsync(blobid);
                if (blob == null) throw new ArgumentException(nameof(blobid));

                var byteContent = new ByteArrayContent(blob.Content);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(blob.ContentType);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = byteContent
                };
            }
            catch
            {
                throw new HttpException(400, "The given parameter(s) are invalid");
            }
        }
    }
}
