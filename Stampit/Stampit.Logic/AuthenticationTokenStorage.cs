using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic
{
    public class AuthenticationTokenStorage : IAuthenticationTokenStorage
    {
        #region Singleton
        private static AuthenticationTokenStorage _instance;
        private AuthenticationTokenStorage()
        {
            this.LoggedInUsers = new Dictionary<string, string>();
        }
        public static AuthenticationTokenStorage GetAuthenticationTokenStorage()
        {
            if(_instance == null)
            {
                lock(typeof(AuthenticationTokenStorage))
                {
                    if (_instance == null)
                        _instance = new AuthenticationTokenStorage();
                    return _instance;
                }
            }
            return _instance;
        }
        #endregion

        public IDictionary<string, string> LoggedInUsers { get; }

        public string GenerateSessionToken(string usermail)
        {
            var newSessionToken = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            LoggedInUsers.Add(newSessionToken, usermail);
            return newSessionToken;
        }

        public void RevokeSessionToken(string usermail)
        {
            if (LoggedInUsers.Values.Contains(usermail))
                foreach (var entry in LoggedInUsers.Where(entry => entry.Value == usermail))
                    LoggedInUsers.Remove(entry);
        }
    }
}
