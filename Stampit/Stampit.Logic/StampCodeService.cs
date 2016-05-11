using Stampit.CommonType;
using Stampit.Entity;
using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic
{
    public class StampCodeService : IStampCodeService
    {
        private IStampCodeStorage StampCodeStorage { get; }
        private IStampcardRepository StampcardRepository { get; }

        public StampCodeService(IStampCodeStorage stampCodeStorage, IStampcardRepository stampcardRepository)
        {
            this.StampCodeStorage = stampCodeStorage;
            this.StampcardRepository = stampcardRepository;
        }

        public async Task ScanCodeAsync(string code, Enduser scanner)
        {
            if (scanner == null)
                throw new ArgumentNullException("scanner");

            if (!StampCodeStorage.ExistsCode(code))
                throw new IllegalCodeException(code);

            try
            {
                var products = StampCodeStorage.UseStampCode(code);

                foreach (var productPair in products)
                {
                    var product = productPair.Key;
                    for (int i = 0; i < productPair.Value; i++)
                    {
                        var stampcards = await StampcardRepository.GetAllStampcardsFromProduct(scanner, product);
                        if(stampcards.Any())
                        {
                            var latestStampcard = stampcards.FirstOrDefault();
                            if (latestStampcard.Stamps.Count == product.RequiredStampCount)
                                latestStampcard = new Stampcard { Enduser = scanner, IsRedeemed = false, Product = product };
                            latestStampcard.Stamps.Add(new Stamp { Stampcard = latestStampcard });
                            await StampcardRepository.CreateOrUpdateAsync(latestStampcard);
                        }
                        else
                        {
                            var newStampcard = new Stampcard { Enduser = scanner, IsRedeemed = false, Product = product, Stamps = new List<Stamp>() };
                            newStampcard.Stamps.Add(new Stamp { Stampcard = newStampcard });
                            await StampcardRepository.CreateOrUpdateAsync(newStampcard);
                        }
                    }
                }
            } catch (IllegalCodeException)
            {
                var product = StampCodeStorage.UseRedemtionCode(code);

                var stampcards = (await StampcardRepository.GetAllStampcardsFromProduct(scanner, product))
                    .Where(sc => sc.Stamps.Count >= product.RequiredStampCount && !sc.IsRedeemed)
                    .OrderBy(sc => sc.CreatedAt);
                if (!stampcards.Any()) throw new NotRedeemableStampcardException(code);

                var stampcard = stampcards.FirstOrDefault();
                if (stampcard == null)
                    throw new NotRedeemableStampcardException(code);
                stampcard.IsRedeemed = true;
                await StampcardRepository.CreateOrUpdateAsync(stampcard);
            }
        }

        public void AddStampcode(string stampcode, IDictionary<Product,int> products)
        {
            StampCodeStorage.AddStampcode(stampcode, products);
        }

        public void AddReedemtionStampcode(string stampcode, Product product)
        {
            StampCodeStorage.AddRedemtioncode(stampcode, product);
        }
    }
}
