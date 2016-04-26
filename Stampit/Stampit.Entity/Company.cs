using System;
using System.Collections.Generic;
	
namespace Stampit.Entity
{
	public class Company : Entity
	{
		public string CompanyName { get; set; }
        public string ContactAddress { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Businessuser> Businessusers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Blob Blob { get; set; }
	    public string BlobId { get; set; }
	}
}
