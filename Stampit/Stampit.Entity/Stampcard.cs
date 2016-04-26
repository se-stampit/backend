using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Stampcard : Entity
    {
        public bool IsRedeemed{ get; set; }
       
        public virtual ICollection<Stamp> Stamps { get; set; }
        public virtual Product Product { get; set; }
	    public string ProductId { get; set; }
        public virtual Enduser Enduser { get; set; }
	    public string EnduserId { get; set; }
    }
}