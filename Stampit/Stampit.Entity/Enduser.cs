using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stampit.Entity
{
    public class Enduser : Entity
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "E-Mail")]
        public string MailAddress { get; set; }
        
        public virtual ICollection<Stampcard> Stampcards { get; set; }
        public virtual ICollection<Loginprovider> Loginproviders { get; set; }
    }
}