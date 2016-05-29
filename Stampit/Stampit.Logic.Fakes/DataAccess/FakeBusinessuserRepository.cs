using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Fakes
{
    public class FakeBusinessuserRepository : FakeBaseRepository<Businessuser>, IBusinessuserRepository
    {
        private IRoleRepository RoleRepository { get; }
        private ICompanyRepository CompanyRepository { get; }

        public FakeBusinessuserRepository(IRoleRepository roleRepository, ICompanyRepository companyRepository)
        {
            this.RoleRepository = roleRepository;
            this.CompanyRepository = companyRepository;
            this.Data[0].Role = RoleRepository.GetAllAsync(0).Result.Where(r => r.RoleName == "Manager").FirstOrDefault();
            this.Data[0].RoleId = this.Data[0].Role.Id;
            this.Data[0].Company = CompanyRepository.GetAllAsync(0).Result.Where(r => r.Id == "ID123").FirstOrDefault();
            this.Data[0].CompanyId = this.Data[0].Company.Id;
            this.Data[1].Role = RoleRepository.GetAllAsync(0).Result.Where(r => r.RoleName == "KioskUser").FirstOrDefault();
            this.Data[1].RoleId = this.Data[1].Role.Id;
            this.Data[1].Company = CompanyRepository.GetAllAsync(0).Result.Where(r => r.Id == "ID123").FirstOrDefault();
            this.Data[1].CompanyId = this.Data[1].Company.Id;
        }

        protected override IList<Businessuser> Data { get; } = new List<Businessuser>
        {
            new Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                FirstName="Mr.",
                LastName="Fussal",
                MailAddress="fussal@gmx.at",
                CreatedAt = DateTime.Now
            },
            new Businessuser()
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                FirstName="Shop",
                LastName="Account",
                MailAddress="shop@gmx.at",
                CreatedAt = DateTime.Now
            }
        };
    }
}
