using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
	public class Product : Entity
	{
		public string Productname { get; set; }
		public int RequiredStampCount { get; set; }
		public string BonusDescription { get; set; }
		public double Price { get; set; }
		public int MaxDuration { get; set; }
		public bool Active { get; set; }
		
		public virtual ICollection<Stampcard> Stampcards { get; set; }
		public virtual Company Company { get; set; }
		public string CompanyId { get; set; }
		
	}
}