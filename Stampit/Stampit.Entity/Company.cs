using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;	

namespace Stampit.Entity
{
	public class Company : Entity
	{
        [Display(Name = "Company Name")]
        [Required]
		public string CompanyName { get; set; }
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Businessuser> Businessusers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Blob Blob { get; set; }
	    public string BlobId { get; set; }
	}
}
