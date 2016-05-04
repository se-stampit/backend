using Stampit.CommonType;
using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface.DataAccess
{
    /// <summary>
    /// The repository of all stampcards
    /// </summary>
    interface IStampcardRepository : IRepository<Stampcard>
    {
        /// <summary>
        /// Finds the stampcards of the given user
        /// </summary>
        /// <param name="company">The user to define which stampcards should be returned</param>
        /// <param name="pagenr">The number of the page which is requested</param>
        /// <param name="pagesize">The size of each page, default value is 10</param>
        /// <returns>The list of the requested stampcards</returns>
        Task<IEnumerable<Stampcard>> GetAllStampcards(Enduser user, int page, int pagesize = Setting.DEFAULT_PAGE_SIZE);
    }
}
