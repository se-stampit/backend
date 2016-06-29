using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;
using Stampit.DataAccess;
using System.Data.Entity;

namespace Stampit.Logic.DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity.Entity
    {
        protected StampitDbContext DbContext { get; }
        protected DbSet<T> Set
        {
            get
            {
                return DbContext.Set<T>();
            }
        }
        
        public BaseRepository()
        {

        }

        public Task<long> Count()
        {
            return Set.LongCountAsync();
        }

        public async Task CreateOrUpdateAsync(T entity)
        {
            if (entity == null)
                return;
            if (await Set.FindAsync(entity.Id) == null)
                Set.Add(entity);
            else
                DbContext.Entry(entity).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            Set.Remove(entity);
            return DbContext.SaveChangesAsync();
        }

        public Task<T> FindByIdAsync(string id)
        {
            return Set.FindAsync(id);
        }

        public Task<IEnumerable<T>> GetAllAsync(int pagenr, int pagesize = 100)
        {
            return Task.FromResult(Set.Skip(pagenr * pagesize).Take(pagesize).AsEnumerable());
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.DbContext.Dispose();
                }

                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
