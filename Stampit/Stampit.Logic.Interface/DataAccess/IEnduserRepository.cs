using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Interface
{
    public interface IEnduserRepository : IRepository<Enduser>
    {
        Task<Enduser> FindByMailAddress(string mailaddress);
    }
}