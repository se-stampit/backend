using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Models
{
    public class UsersViewModel
    {
        public Businessuser User{ get; set; }
        public IEnumerable<SelectListItem> Roles = new List<SelectListItem>();
    }
}