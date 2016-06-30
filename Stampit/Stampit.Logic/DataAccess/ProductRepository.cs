using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.CommonType;

namespace Stampit.Logic.DataAccess
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public Task<IEnumerable<Product>> FindProductsFromCompany(Company company, int pagenr, int pagesize = 100)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            var products = from product in Set
                          where product.CompanyId == company.Id
                         select product;

            return Task.FromResult(products.AsEnumerable());
        }

        public Task<IDictionary<Product, double>> SalesPerProduct(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            var sales = (from product in Set
                         join stampcard in DbContext.Stampcards on product.Id equals stampcard.ProductId
                         join stamp in DbContext.Stamps on stampcard.Id equals stamp.StampcardId
                         group product by product into productgroup
                         select new Tupel<Product, double>
                         {
                             Arg1 = productgroup.Key,
                             Arg2 = productgroup.LongCount() * productgroup.Key.Price
                         }).ToList();
            
            return Task.FromResult(sales.ToDictionaryFromKeyValuePair());
        }
    }
}
