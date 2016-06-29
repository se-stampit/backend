using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.DataAccess
{
    public class StampcardRepository : BaseRepository<Stampcard>, IStampcardRepository
    {
        public Task<int> CountRedeemedStampcardsFromCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountStampcardsFromCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcards(Enduser user, int page, int pagesize = 100)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<Product, IDictionary<int, int>>> GetAllStampcardsFromCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcardsFromProduct(Enduser user, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
