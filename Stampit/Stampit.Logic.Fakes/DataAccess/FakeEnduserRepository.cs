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
                FirstName = "Max",
                LastName = "Mustermann",
                Loginproviders = new List<Loginprovider>()
                {
                    new Loginprovider
                    {
                        Id = Guid.NewGuid().ToString().Replace("-",""),
                        CreatedAt = DateTime.Now,
                        AuthId = "test",
                        AuthService = "fb"
                    }
                },
                MailAddress = "max@mustermann.at",
                Stampcards = new List<Stampcard>()
            },

            new Enduser
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                FirstName = "Mona",
                LastName = "Musterfrau",
                Loginproviders = new List<Loginprovider>()
                {
                    new Loginprovider
                    {
                        Id = Guid.NewGuid().ToString().Replace("-",""),
                        CreatedAt = DateTime.Now,
                        AuthId = "monaMuster",
                        AuthService = "fb"
                    }
                },
                MailAddress = "mona@musterfrau.at",
                Stampcards = new List<Stampcard>()
            },

            new Enduser
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                FirstName = "Mimmi",
                LastName = "Musterkind",
                Loginproviders = new List<Loginprovider>()
                {
                    new Loginprovider
                    {
                        Id = Guid.NewGuid().ToString().Replace("-",""),
                        CreatedAt = DateTime.Now,
                        AuthId = "mimmiMuster",
                        AuthService = "fb"
                    }
                },
                MailAddress = "mimmi@musterkind.at",
                Stampcards = new List<Stampcard>()
            }
        };

        public Task<Enduser> FindByMailAddress(string mailaddress)
        {
            return Task.FromResult(Data.Where(user => user.MailAddress == mailaddress).FirstOrDefault());
        }

        public Task<long> CountEnduser(Company company)
        {
            long enduser = 3;
            return Task.FromResult(enduser);
        }
    }
}
