using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Stampit.Entity
{
    public class Loginprovider : Entity
    {
        [JsonProperty("authprovider")]
        public string AuthService { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("user")]
        public virtual Enduser Enduser { get; set; }
	    public string EnduserId { get; set; }
    }
}