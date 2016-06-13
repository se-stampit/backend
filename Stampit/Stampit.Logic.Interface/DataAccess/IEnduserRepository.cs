using Stampit.Entity;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    public interface IEnduserRepository : IRepository<Enduser>
    {
        /// <summary>
        /// Counts the endusers of the given company
        /// </summary>
        /// <param name="company">The company to define which endusers should be returned</param>
        /// <returns>Count of endusers</returns>
        Task<long> CountEnduser(Company company);
    }
}