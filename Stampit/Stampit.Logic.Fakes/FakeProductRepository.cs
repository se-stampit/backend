using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;
using Stampit.Logic.Interface;

namespace Stampit.Logic.Fakes
{
    public class FakeProductRepository : IProductRepository
    {
        private static IEnumerable<Product> products = new List<Product>
        {
            new Product()
            {
                Company=FakeCompanyRepository.Companies.FirstOrDefault(),
                Id=Guid.NewGuid().ToString(),
                Productname="Coffee",
                Price=2.5,
                Active=true,
                BonusDescription="Get one free coffee",
                RequiredStampCount=10,
                MaxDuration=365
            },
            new Product()
            {
                Company=FakeCompanyRepository.Companies.FirstOrDefault(),
                Id =Guid.NewGuid().ToString(),
                Productname="Tea",
                Price=2,
                Active=true,
                BonusDescription="Get one free tea",
                RequiredStampCount=5,
                MaxDuration=365
            },
            new Product()
            {
                Company=FakeCompanyRepository.Companies.LastOrDefault(),
                Id =Guid.NewGuid().ToString(),
                Productname="Kebap",
                Price=5,
                Active=true,
                BonusDescription="Get one free kebap",
                RequiredStampCount=10,
                MaxDuration=365
            },
                        new Product()
            {
                Company=FakeCompanyRepository.Companies.LastOrDefault(),
                Id =Guid.NewGuid().ToString(),
                Productname="Pizza",
                Price=7,
                Active=true,
                BonusDescription="Get one free pizza",
                RequiredStampCount=10,
                MaxDuration=365
            }
        }.AsReadOnly();

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
            var companyProducts = (from p in products
                                   where p.Company?.Id == company?.Id
                                   select p).Skip(pagenr * pagesize).Take(pagesize);

            return Task.FromResult(companyProducts);
        }

        public Task<IEnumerable<Product>> GetAllAsync(int pagenr, int pagesize = 10)
        {
            var companyProducts = (from p in products
                                   select p).Skip(pagenr * pagesize).Take(pagesize);

            return Task.FromResult(companyProducts);
        }
    }
}
