using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Stamp : Entity
    {
        public virtual Stampcard Stampcard { get; set; }
	    public string StampcardId { get; set; }
    }
}