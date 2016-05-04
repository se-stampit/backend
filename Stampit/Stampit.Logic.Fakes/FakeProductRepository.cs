using Stampit.Logic.Interface.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Fakes
{
    public class FakeProductRepository : IProductRepository
    {
        public Task CreateOrUpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FindProductsFromCompany(Company company, int pagenr, int pagesize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync(int pagenr, int pagesize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
