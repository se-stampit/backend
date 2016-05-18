using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;
using Stampit.Logic.Interface;

namespace Stampit.Logic.Fakes
{
    public class FakeProductRepository : FakeBaseRepository<Product>, IProductRepository
    {
        private ICompanyRepository CompanyRepository { get; }
        private Company TestCompany1 { get; }
        private Company TestCompany2 { get; }

        public FakeProductRepository(ICompanyRepository companyRepository)
        {
            this.CompanyRepository = companyRepository;
            var companies = CompanyRepository.GetAllAsync(0).Result; //Result only allowed because of fakerepository which returns the result immediatly and no state machine is generated
            this.TestCompany1 = companies.FirstOrDefault();
            this.TestCompany2 = companies.LastOrDefault();
            for (int i = 0; i < this.Data.Count; i++)
            {
                if (i < 2)
                    Data[i].Company = TestCompany1;
                else
                    Data[i].Company = TestCompany2;
                Data[i].CompanyId = Data[i].Company.Id;
            }
        }

        protected override IList<Product> Data { get; } = new List<Product>
        {
            new Product()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Productname = "Coffee",
                Price = 2.5,
                Active = true,
                BonusDescription = "Get one free coffee",
                RequiredStampCount = 10,
                MaxDuration = 365,
                Stampcards = new List<Stampcard>()
            },
            new Product()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Productname = "Tea",
                Price = 2,
                Active = true,
                BonusDescription = "Get one free tea",
                RequiredStampCount = 5,
                MaxDuration = 365,
                Stampcards = new List<Stampcard>()
            },
            new Product()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Productname = "Kebap",
                Price = 5,
                Active = true,
                BonusDescription = "Get one free kebap",
                RequiredStampCount = 10,
                MaxDuration = 365,
                Stampcards = new List<Stampcard>()
            },
            new Product()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Productname = "Pizza",
                Price = 7,
                Active = true,
                BonusDescription = "Get one free pizza",
                RequiredStampCount = 10,
                MaxDuration = 365,
                Stampcards = new List<Stampcard>()
            }
        };

        public Task<IEnumerable<Product>> FindProductsFromCompany(Company company, int pagenr, int pagesize = 10)
        {
            var companyProducts = (from p in Data
                                   where p.CompanyId == company?.Id
                                      && !string.IsNullOrEmpty(company?.Id)
                                   select p).Skip(pagenr * pagesize).Take(pagesize);

            return Task.FromResult(companyProducts);
        }
    }
}
