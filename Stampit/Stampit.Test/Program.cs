using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Plus.v1;
using Stampit.CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stampit.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ClientSecrets secrets = new ClientSecrets
            {
                ClientId = Setting.CLIENT_ID,
                ClientSecret = Setting.CLIENT_SECRET
            };
            string[] scopes = { PlusService.Scope.PlusLogin, PlusService.Scope.UserinfoEmail, PlusService.Scope.UserinfoProfile };

            IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = scopes
            });

            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None).Result;

            var accesstoken = credential.GetAccessTokenForRequestAsync().Result;

            Console.WriteLine(accesstoken);
            Console.ReadLine();
        }
    }
}
