using Stampit.CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Stampit.Service.Controllers
{
    public class ScanController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult Scan(string code)
        {
            return Ok();
        }
    }
}
