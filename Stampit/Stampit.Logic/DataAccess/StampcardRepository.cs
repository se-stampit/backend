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
            if (company == null) throw new ArgumentNullException(nameof(company));


        }

        public Task<int> CountStampcardsFromCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcards(Enduser user, int page, int pagesize = 100)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
        }

        public Task<IDictionary<Product, IDictionary<int, int>>> GetAllStampcardsFromCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcardsFromProduct(Enduser user, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
