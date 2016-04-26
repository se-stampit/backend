using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
	public class Businessuser : Entity
	{
		public virtual Role Role { get; set; }
	    public string RoleId { get; set; }
    	public virtual Company Company { get; set; }
	    public string CompanyId { get; set; }	
	}
}