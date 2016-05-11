using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stampit.Webapp.Models
{
    public class ScanViewModel
    {
        public string Base64Img { get; set; }
        public string Stampcode { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}