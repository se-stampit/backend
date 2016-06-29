using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.DataAccess
{
    public class StampitDbContext : DbContext
    {
        public StampitDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Blob> Blobs { get; set; }
        public DbSet<Businessuser> Businessusers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Enduser> Endusers { get; set; }
        public DbSet<Loginprovider> Loginprovider { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stamp> Stamps { get; set; }
        public DbSet<Stampcard> Stampcards { get; set; }
        public DbSet<Store> Stores { get; set; }

        public override int SaveChanges()
        {
            UpdateChangeTimes();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateChangeTimes();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            UpdateChangeTimes();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangeTimes()
        {
            var modifiedEntries = from entry in ChangeTracker.Entries()
                                  where entry.Entity is Entity.Entity
                                     && (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                                  select entry;

            foreach (var entry in modifiedEntries)
            {
                Entity.Entity entity = entry.Entity as Entity.Entity;
                if (entity != null)
                {
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.Id = Guid.NewGuid().ToString().Replace("-", "");
                        entity.CreatedAt = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    }

                    entity.UpdatedAt = now;
                }
            }
        }
    }
}
