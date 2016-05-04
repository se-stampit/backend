using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Fakes
{
    public class FakeCompanyRepository : IRepository<Company>
    {
       internal static IEnumerable<Company> Companies = new List<Company>
        {
            new Company()
                {
                    Id=Guid.NewGuid().ToString(),
                    CompanyName="CoffeeRoom",
                    ContactName="CoffeeMaster",
                    Description="Nice coffee house"
                },
            new Company()
                {
                    Id=Guid.NewGuid().ToString(),
                    CompanyName="KebapHouse",
                    ContactName="KebapMan",
                    Description="Nice kebap house"
                }
        }.AsReadOnly();

        public Task CreateOrUpdateAsync(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task<Company> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetAllAsync(int pagenr, int pagesize = 10)
        {
            var companies = (from c in Companies
                            select c).Skip(pagenr * pagesize).Take(pagesize);

            return Task.FromResult(companies);
        }
    }
}
