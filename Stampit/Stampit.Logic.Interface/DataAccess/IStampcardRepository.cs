using Stampit.CommonType;
using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// The repository of all stampcards
    /// </summary>
    public interface IStampcardRepository : IRepository<Stampcard>
    {
        /// <summary>
        /// Finds the stampcards of the given user, also included are all stamps
        /// </summary>
        /// <param name="user">The user to define which stampcards should be returned</param>
        /// <param name="pagenr">The number of the page which is requested</param>
        /// <param name="pagesize">The size of each page, default value is 10</param>
        /// <returns>The list of the requested stampcards which are orderd descending by the amount of gained stamps</returns>
        Task<IEnumerable<Stampcard>> GetAllStampcards(Enduser user, int page, int pagesize = Setting.DEFAULT_PAGE_SIZE);
        /// <summary>
        /// Finds the stampcards of the given user of the given product, also included are all stamps
        /// </summary>
        /// <param name="user">The user to define which stampcards should be returned</param>
        /// <param name="product">The product which is assoziated with the stampcards</param>
        /// <returns>The list of the requested stampcards which are orderd descending by the amount of gained stamps</returns>
        Task<IEnumerable<Stampcard>> GetAllStampcardsFromProduct(Enduser user, Product product);
        /// <summary>
        /// Generates a prepared statistic for the given company based on their stampcard filling level for each product
        /// </summary>
        /// <param name="company">The company for which the statistic should be created</param>
        /// <returns>A dictionary of all stampcards defined as product with their related stampcards respectively to their current stampcount
        /// The key (x-axis) of the inner dictionary defines the current fillcount of stamps of the stampcards
        /// The value (y-axis) of the inner dictionary defines the count of stampcards which have the fillcount given as key
        /// </returns>
        Task<IDictionary<Product, IDictionary<int,int>>> GetAllStampcardsFromCompany(Company company);
        /// <summary>
        /// Counts the stampcards in circulation of the given company
        /// </summary>
        /// <param name="company">The company to define which amount should be returned</param>
        /// <returns>Returns the amount of the total stampcards in circulation</returns>
        Task<int> CountStampcardsFromCompany(Company company);
        /// <summary>
        /// Counts the redeemed stampcards of the given company
        /// </summary>
        /// <param name="company">The company to define which amount should be returned</param>
        /// <returns>Returns the amount of the redeemed stampcards</returns>
        Task<int> CountRedeemedStampcardsFromCompany(Company company);
    }
}
