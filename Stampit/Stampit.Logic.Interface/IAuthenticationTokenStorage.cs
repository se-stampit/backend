using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    public interface IAuthenticationTokenStorage
    {
        /// <summary>
        /// For each sessiontoken, the user's mailaddress is specified
        /// </summary>
        IDictionary<string,string> LoggedInUsers { get; }

        /// <summary>
        /// Generates a new sessiontoken for authentication and stores it in the LoggedInUsers dictionary
        /// </summary>
        /// <param name="usermail">The mail to identify the user</param>
        /// <returns>A new generated sessiontoken for the upcoming requests</returns>
        string GenerateAuthToken(string usermail);
    }
}
