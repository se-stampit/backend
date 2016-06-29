using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stampit.Entity
{
    public class Enduser : Entity
    {	
        [JsonProperty("firstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [JsonProperty("mailAddress")]
        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "The E-Mail address is required")]
        [EmailAddress(ErrorMessage = "Invalid E-Mail Address")]
        public string MailAddress { get; set; }
        
        public virtual ICollection<Stampcard> Stampcards { get; set; }
        public virtual ICollection<Loginprovider> Loginproviders { get; set; }
    }
}
