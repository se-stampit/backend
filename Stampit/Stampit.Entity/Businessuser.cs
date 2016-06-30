using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stampit.Entity
{
	public class Businessuser : Enduser
	{
		public virtual Role Role { get; set; }
		public string RoleId { get; set; }
		public virtual Company Company { get; set; }
		public string CompanyId { get; set; }	
	}
}