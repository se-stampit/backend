using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.DataAccess
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public Task<IEnumerable<Store>> GetStoresOfCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            return Task.FromResult(company.Stores.AsEnumerable());
        }

        public Task<long> GetStoresOfCompanyCount(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            return Task.FromResult(company.Stores.LongCount());
        }
    }
}
