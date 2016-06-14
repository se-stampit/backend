using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stampit.Service.Middleware
{
    public enum RequestAuthenticationMode
    {
        REGISTER,
        LOGIN,
        SESSIONTOKEN,
        NONE
    }
}