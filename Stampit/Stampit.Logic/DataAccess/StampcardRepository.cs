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
    public class StampcardRepository : BaseRepository<Stampcard>, IStampcardRepository
    {
        public Task<int> CountRedeemedStampcardsFromCompany(Company param_company)
        {
            if (param_company == null) throw new ArgumentNullException(nameof(param_company));

            var redeemedCount = (from stampcard in Set
                                 join product in DbContext.Products on stampcard.ProductId equals product.Id
                                 join company in DbContext.Companies on product.CompanyId equals company.Id
                                 where company == param_company
                                    && stampcard.IsRedeemed
                                 select stampcard).Count();
            return Task.FromResult(redeemedCount);
        }

        public Task<int> CountStampcardsFromCompany(Company param_company)
        {
            if (param_company == null) throw new ArgumentNullException(nameof(param_company));

            var stampcardCount = (from stampcard in Set
                                 join product in DbContext.Products on stampcard.ProductId equals product.Id
                                 join company in DbContext.Companies on product.CompanyId equals company.Id
                                 where company == param_company
                                 select stampcard).Count();
            return Task.FromResult(stampcardCount);
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcards(Enduser user, int page, int pagesize = 100)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var stampcards = (from stampcard in Set
                             where stampcard.EnduserId == user.Id
                                && !string.IsNullOrEmpty(user.Id)
                           orderby stampcard.Stamps.Count descending
                            select stampcard).Skip(page * pagesize).Take(pagesize);

            return Task.FromResult(stampcards.AsEnumerable());
        }

        public Task<IDictionary<Product, IDictionary<int, int>>> GetAllStampcardsFromCompany(Company param_company)
        {
            if (param_company == null) throw new ArgumentNullException(nameof(param_company));

            var stampcardStatus = from stampcard in Set
                                  join stamp in DbContext.Stamps on stampcard.Id equals stamp.StampcardId
                                  join product in DbContext.Products on stampcard.ProductId equals product.Id
                                  where product.CompanyId == param_company.Id
                                  group stamp by product into stampgroup
                                  select new KeyValuePair<Product, IDictionary<int, int>>
                                  (
                                      stampgroup.Key,
                                      (from stamp in stampgroup.IndexSequence(1, stampgroup.Key.RequiredStampCount)
                                      group stamp by stamp.Value.StampcardId into g
                                        let stampcount = g.Count()
                                        let index = g.First().Key
                                      where stampcount == index
                                      group stampcount by index into g2
                                     select new KeyValuePair<int,int>(g2.Key, g2.Count())).ToDictionaryFromKeyValuePair()
                                  );

            return Task.FromResult(stampcardStatus.ToDictionaryFromKeyValuePair());
        }

        public Task<IEnumerable<Stampcard>> GetAllStampcardsFromProduct(Enduser user, Product product)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (product == null) throw new ArgumentNullException(nameof(product));

            var stampcards = from stampcard in Set
                            where stampcard.EnduserId == user.Id
                               && !string.IsNullOrEmpty(user.Id)
                               && stampcard.ProductId == product.Id
                               && !string.IsNullOrEmpty(product.Id)
                          orderby stampcard.Stamps.Count
                           select stampcard;

            return Task.FromResult(stampcards.AsEnumerable());
        }
    }
}
