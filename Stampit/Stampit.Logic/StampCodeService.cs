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

        public StampCodeService(IStampCodeStorage stampCodeStorage)
        {
            this.StampCodeStorage = stampCodeStorage;
        }

        public Task ScanCodeAsync(string code, Enduser scanner)
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
                        //await stampcardRepo
                        //if(any())
                        // order by stampcount
                        // take first
                        // if(count == maxcount)
                        //  new stampcard
                        // add stamp (to new stampcard or first where count < maxcount)
                        //else
                        // new stampcard -> add stamp
                        //-------------------- for each product with scanner
                        
                    }
                }
                throw new NotImplementedException();
            } catch(IllegalCodeException)
            {
                var product = StampCodeStorage.UseRedemtionCode(code);
                //if redemtioncode valid
                // find stampcards with scanner and product
                // take first where count == maxcount
                // if(any())
                //  set stampcard isRedeemed = false
                //  return positiv value
                // else
                //  throw userdefined exception for not redeemable

                throw new NotImplementedException();
            }
        }
    }
}
