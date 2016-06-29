using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stampit.Entity
{
	public class Product : Entity
	{
        [Display(Name = "Name")]
        [Required]
        public string Productname { get; set; }
        [Display(Name = "Stamp Count")]
        [Required]
        public int RequiredStampCount { get; set; }
        [Display(Name = "Bonus")]
		public string BonusDescription { get; set; }
        public double Price { get; set; }
        [Display(Name = "Max. Duration")]
        [Required]
        public int MaxDuration { get; set; }
		public bool Active { get; set; }
		
		public virtual ICollection<Stampcard> Stampcards { get; set; }
		public virtual Company Company { get; set; }
		public string CompanyId { get; set; }
		
	}
}