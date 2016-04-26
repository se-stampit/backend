using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Loginprovider : Entity
    {
        public string AuthService { get; set; }
        public string AuthId { get; set; }
        
        public virtual Enduser Enduser { get; set; }
	    public string EnduserId { get; set; }
    }
}