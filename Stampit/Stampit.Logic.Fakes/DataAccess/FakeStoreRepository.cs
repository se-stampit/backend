using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Fakes
{
    public class FakeStoreRepository : FakeBaseRepository<Store>, IStoreRepository
    {
        private ICompanyRepository CompanyRepository { get; }

        public FakeStoreRepository(ICompanyRepository companyRepository)
        {
            this.CompanyRepository = companyRepository;
            var companies = this.CompanyRepository.GetAllAsync(0).Result;
            Data.First().CompanyId = companies.FirstOrDefault()?.Id;
            Data.First().Company = companies.FirstOrDefault();
            Data.Last().CompanyId = companies.LastOrDefault()?.Id;
            Data.Last().Company = companies.LastOrDefault();
        }

        protected override IList<Store> Data { get; } = new List<Store>
        {
            new Store
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Address = "Coffeestreet",
                Latitude = 48.2,
                Longitude = 12.4
            },
            new Store
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                CreatedAt = DateTime.Now,
                Address = "Kebapstreet",
                Latitude = 48.3,
                Longitude = 12.3
            }
        };

        public Task<IEnumerable<Store>> GetStoresOfCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            return Task.FromResult
            (
                Data.Where(store => store.Company == company || (store.CompanyId == company.Id && store.CompanyId != null))
            );
        }

        public Task<long> GetStoresOfCompanyCount(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            return Task.FromResult
            (
                Data.LongCount(store => store.Company == company || (store.CompanyId == company.Id && store.CompanyId != null))
            );
        }
    }
}
