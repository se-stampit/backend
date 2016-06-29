using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public class FakeRoleRepository : FakeBaseRepository<Role>, IRoleRepository
    {
        protected override IList<Role> Data { get; } = new List<Role>
        {
            new Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                RoleName = "Manager",
                CreatedAt = DateTime.Now
            },
            new Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                RoleName = "KioskUser",
                CreatedAt = DateTime.Now
            },
            new Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                RoleName = "Admin",
                CreatedAt = DateTime.Now
            },
            new Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                RoleName = "None",
                CreatedAt = DateTime.Now
            }
        };
    }
}
