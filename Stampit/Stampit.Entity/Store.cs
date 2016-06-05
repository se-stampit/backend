using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Store : Entity
    {
        public string StoreName { get; set; }
        public string Address{ get; set; }
        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
        public string Description { get; set; }

        public virtual Company Company { get; set; }
	    public string CompanyId { get; set; }
    }
}