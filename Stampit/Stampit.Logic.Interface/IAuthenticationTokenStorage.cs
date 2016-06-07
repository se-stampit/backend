﻿using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    public interface IAuthenticationTokenStorage
    {
        IDictionary<string,Enduser> LoggedInUsers { get; }
        string GenerateAuthToken(Enduser user);
    }
}
