using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Enduser : Entity
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("mailAddress")]
        public string MailAddress { get; set; }
        
        public virtual ICollection<Stampcard> Stampcards { get; set; }
        public virtual ICollection<Loginprovider> Loginproviders { get; set; }
    }
}