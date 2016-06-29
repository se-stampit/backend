using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Models
{
    public class AdminCompanySelectorViewModel
    {
        public Company Company { get; set; }
        public IEnumerable<SelectListItem> Companies = new List<SelectListItem>();
    }
}