using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.DataAccess
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public Task<IEnumerable<Product>> FindProductsFromCompany(Company company, int pagenr, int pagesize = 100)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<Product, double>> SalesPerProduct(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
