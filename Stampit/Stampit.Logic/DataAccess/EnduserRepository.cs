using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.DataAccess
{
    public class EnduserRepository : BaseRepository<Enduser>, IEnduserRepository
    {
        public Task<long> CountEnduser(Company param_company)
        {
            var count = (from user in Set
                         join card in DbContext.Stampcards on user.Id equals card.EnduserId
                         join product in DbContext.Products on card.ProductId equals product.Id
                         join company in DbContext.Companies on product.CompanyId equals company.Id
                        where param_company.Id == company.Id
                       select user).LongCount();
            return Task.FromResult(count);
        }

        public Task<Enduser> FindByMailAddress(string mailaddress)
        {
            var user =
                (from u in Set
                where mailaddress == u.MailAddress
               select u).FirstOrDefault();
            return Task.FromResult(user);
        }
    }
}
