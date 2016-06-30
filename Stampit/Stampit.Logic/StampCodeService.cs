using Stampit.CommonType;
using Stampit.Entity;
using Stampit.Logic.Interface;
using Stampit.Logic.Interface.DataAccess;
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
        private IStampRepository StampRepository { get; }

        public StampCodeService(IStampCodeStorage stampCodeStorage, IStampcardRepository stampcardRepository, IStampRepository stampRepository)
        {
            this.StampCodeStorage = stampCodeStorage;
            this.StampcardRepository = stampcardRepository;
            this.StampRepository = stampRepository;
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
                            var stampcount = (await StampRepository.GetAllAsync(0)).Where(s => s.StampcardId == latestStampcard.Id).Count();
                            if (stampcount == product.RequiredStampCount)
                                latestStampcard = new Stampcard
                                {
                                    EnduserId = scanner.Id,
                                    IsRedeemed = false,
                                    ProductId = product.Id,
                                    Stamps = new List<Stamp>()
                                };
                            await StampcardRepository.CreateOrUpdateAsync(latestStampcard);
                            await StampRepository.CreateOrUpdateAsync(new Stamp { StampcardId = latestStampcard.Id });
                        }
                        else
                        {
                            var newStampcard = new Stampcard
                            {
                                EnduserId = scanner.Id,
                                IsRedeemed = false,
                                ProductId = product.Id,
                                Stamps = new List<Stamp>()
                            };
                            await StampcardRepository.CreateOrUpdateAsync(newStampcard);
                            await StampRepository.CreateOrUpdateAsync(new Stamp { StampcardId = newStampcard.Id });
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
