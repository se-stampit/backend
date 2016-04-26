using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Enduser : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        
        public virtual ICollection<Stampcard> Stampcards { get; set; }
        public virtual ICollection<Loginprovider> Loginproviders { get; set; }
    }
}