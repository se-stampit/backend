using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public abstract class FakeBaseRepository<T> : IRepository<T> where T : Entity.Entity
    {
        protected abstract IList<T> Data { get; }

        public Task CreateOrUpdateAsync(T entity)
        {
            var entry = Data.Where(t => t.Id == entity.Id).FirstOrDefault();
            if (entry == null || string.IsNullOrEmpty(entity.Id))
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = null;
                entity.Id = Guid.NewGuid().ToString().Replace("-", "");
                Data.Add(entity);
            }
            else
            {
                int idx = Data.IndexOf(entry);
                entry.UpdatedAt = DateTime.Now;
                Data.RemoveAt(idx);
                Data.Insert(idx, entity);
            }

            return Task.FromResult(new object());
        }

        public Task<T> FindByIdAsync(string id)
        {
            return Task.FromResult
                (
                    (from entity in Data
                    where entity.Id == id
                    select entity).FirstOrDefault()
                );
        }

        public Task<IEnumerable<T>> GetAllAsync(int page, int pagesize = 10)
        {
            return Task.FromResult
                (
                    (from entity in Data
                     select entity).Skip(page * pagesize).Take(pagesize)
                );
        }

        public void Dispose()
        {
        }
    }
}
