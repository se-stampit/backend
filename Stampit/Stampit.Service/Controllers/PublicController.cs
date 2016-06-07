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

namespace Stampit.Service.Controllers
{
    public class PublicController : ApiController
    {
        private IBlobRepository BlobRepository { get; }

        public PublicController(IBlobRepository blobRepository)
        {
            this.BlobRepository = blobRepository;
        }

        [HttpPut]
        [Route("api/login")]
        public async Task<IHttpActionResult> Login([FromBody]Loginprovider loginprovider)
        {
            return Content(HttpStatusCode.OK, new { sessionToken = loginprovider.Token });
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
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
