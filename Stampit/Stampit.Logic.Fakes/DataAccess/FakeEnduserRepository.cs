using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public class FakeEnduserRepository : FakeBaseRepository<Enduser>, IEnduserRepository
    {
        protected override IList<Enduser> Data { get; } = new List<Enduser>
        {
            new Enduser
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                FirstName = "Test",
                LastName = "Tester",
                Loginproviders = new List<Loginprovider>()
                {
                    new Loginprovider
                    {
                        Id = Guid.NewGuid().ToString().Replace("-",""),
                        CreatedAt = DateTime.Now,
                        Token = "test",
                        AuthService = "fb"
                    }
                },
                MailAddress = "test@tester.at",
                Stampcards = new List<Stampcard>()
            }
        };
    }
}
