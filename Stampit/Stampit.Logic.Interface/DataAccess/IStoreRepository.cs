using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    public interface IStoreRepository : IRepository<Store>
    {
        /// <summary>
        /// Returns all stores which belong to the given company
        /// </summary>
        /// <param name="company">The company to retrieve the stores from</param>
        /// <returns>An enumeration of all to the given company related stores</returns>
        Task<IEnumerable<Store>> GetStoresOfCompany(Company company);
        /// <summary>
        /// Returns the count of all stores which belong to the given company
        /// </summary>
        /// <param name="company">The company to retrieve the count of their stores from</param>
        /// <returns>The count of all stores related to the given company</returns>
        Task<long> GetStoresOfCompanyCount(Company company);
    }
}
