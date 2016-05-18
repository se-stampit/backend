using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public class FakeBusinessuserRepository : FakeBaseRepository<Businessuser>, IBusinessuserRepository
    {
        protected override IList<Businessuser> Data { get; } = new List<Businessuser>
        {
            new Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                Role=new Role() {RoleName="Manager"},
                RoleId="Manager",
                FirstName="Mr.",
                LastName="Fussal",
                MailAddress="fussal@gmx.at"
            },
            new Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                Role=new Role() {RoleName="Shop"},
                RoleId="Shop",
                FirstName="Shop",
                LastName="Account",
                MailAddress="shop@gmx.at",
            }
        };
    }
}
