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
            throw new NotImplementedException();
        }

        public Task<long> GetStoresOfCompanyCount(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
