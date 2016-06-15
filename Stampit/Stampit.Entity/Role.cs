using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stampit.Entity
{
    public class Role : Entity
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }        
    }
}