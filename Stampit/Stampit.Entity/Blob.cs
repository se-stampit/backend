using System;
using System.Collections.Generic;
	
namespace Stampit.Entity
{
	public class Blob : Entity
	{
		public byte Content{ get; set; }	
        public string ContentType{ get; set; }
        public long ContentLength{ get; set; }
        public string Filename{ get; set; }
        
        public virtual ICollection Company Companies { get; set; }
	}
}