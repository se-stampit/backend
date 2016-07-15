using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.DataAccess
{
    public class StampitInitializer : DropCreateDatabaseIfModelChanges<StampitDbContext>
    {
        protected override void Seed(StampitDbContext context)
        {
            var kioskMode = new Entity.Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                RoleName = "KioskUser"
            };

            var manager = new Entity.Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                RoleName = "Manager"
            };

            var admin = new Entity.Role()
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                RoleName = "Admin"
            };

            context.Roles.Add(kioskMode);
            context.Roles.Add(manager);
            context.Roles.Add(admin);

            context.Businessusers.Add(new Entity.Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FirstName = "Richard",
                LastName = "Leinweber",
                MailAddress = "r.leinweber@gmail.com",
                Role = admin,
                RoleId = admin.Id
            });

            context.Businessusers.Add(new Entity.Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-", ""),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FirstName = "Jakob",
                LastName = "Mayer",
                MailAddress = "jakob-mayer@gmx.at",
                Role = admin,
                RoleId = admin.Id
            });

            context.Businessusers.Add(new Entity.Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FirstName = "Stampit",
                LastName = "Admin",
                MailAddress = "stampit.adm@gmail.com",
                Role = admin,
                RoleId = admin.Id
            });

            context.SaveChanges();
        }
    }
}
