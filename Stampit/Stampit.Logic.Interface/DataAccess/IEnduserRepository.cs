using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Interface
{
    public interface IEnduserRepository : IRepository<Enduser>
    {
        Task<Enduser> FindByMailAddress(string mailaddress);
        /// <summary>
        /// Counts the endusers of the given company
        /// </summary>
        /// <param name="company">The company to define which endusers should be returned</param>
        /// <returns>Count of endusers</returns>
        Task<long> CountEnduser(Company company);
    }
}
