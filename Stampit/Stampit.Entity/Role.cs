using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Role : Entity
    {
        public string RoleName{ get; set; }
           
        public virtual ICollection Businessuser Businessusers { get; set; }
    }
}