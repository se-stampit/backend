using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public class FakeStoreRepository : FakeBaseRepository<Store>, IStoreRepository
    {
        private ICompanyRepository CompanyRepository { get; }
        private Company TestCompany { get; }

        public FakeStoreRepository(ICompanyRepository companyRepository)
        {
            this.CompanyRepository = companyRepository;
            this.TestCompany = this.CompanyRepository.GetAllAsync(0, 1).Result.First();
            foreach(var store in Data)
            {
                store.Company = TestCompany;
                store.CompanyId = TestCompany.Id;
            }
        }

        protected override IList<Store> Data { get; } = new List<Store>
        {
            new Store()
            {
                Id = Guid.NewGuid().ToString(),
                Latitude = 48.303487,
                Longitude = 14.288781,
                Address = "Landstraße 34"
            },
            new Store()
            {
                Id = Guid.NewGuid().ToString(),
                Latitude = 48.302487,
                Longitude = 14.285555,
                Address = "Landstraße 56"
            }
        };
    }
}
