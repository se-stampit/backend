using Stampit.CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// Defines an interface to deal with persistent data
    /// </summary>
    /// <typeparam name="T">The type of the entity to be stored</typeparam>
    public interface IRepository<T> where T : Entity.Entity
    {
        /// <summary>
        /// Returns all entities of the given page for the entity type T
        /// </summary>
        /// <param name="pagenr">The number of the page which is requested</param>
        /// <param name="pagesize">The size of each page, default value is 10</param>
        /// <returns>The list of the requested entities</returns>
        Task<IEnumerable<T>> GetAllAsync(int pagenr, int pagesize = Setting.DEFAULT_PAGE_SIZE);
        /// <summary>
        /// Finds a particular entity based on the primarykey
        /// </summary>
        /// <param name="id">The primarykey of the entity to be found</param>
        /// <returns>The requested entity</returns>
        Task<T> FindByIdAsync(string id);
        /// <summary>
        /// Creates the given entity if the primarykey hasn't already been inserted or is empty or null
        /// Otherwise the entity will be updated
        /// </summary>
        /// <param name="entity">The entity to be created or updated</param>
        Task CreateOrUpdateAsync(T entity);
    }
}
