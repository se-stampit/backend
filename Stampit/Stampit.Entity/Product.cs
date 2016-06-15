using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stampit.Entity
{
	public class Product : Entity
	{
        [Display(Name = "Name")]
        public string Productname { get; set; }
        [Display(Name = "Stamp Count")]
        public int RequiredStampCount { get; set; }
        [Display(Name = "Bonus")]
		public string BonusDescription { get; set; }
		public double Price { get; set; }
        [Display(Name = "max. Duration")]
        public int MaxDuration { get; set; }
		public bool Active { get; set; }
		
		public virtual ICollection<Stampcard> Stampcards { get; set; }
		public virtual Company Company { get; set; }
		public string CompanyId { get; set; }
		
	}
}