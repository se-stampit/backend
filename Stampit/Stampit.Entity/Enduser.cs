using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Enduser : Entity
    {
        public String FirstName{ get; set; }
        public String LastName{ get; set; }
        public String MailAddress{ get; set; }
        
        public virtual ICollection Stampcard Stampcards { get; set; }
        public virtual ICollection Loginprovider Loginproviders { get; set; }
    }
}