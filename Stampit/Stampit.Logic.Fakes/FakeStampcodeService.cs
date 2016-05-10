using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampit.Entity;

namespace Stampit.Logic.Fakes
{
    public class FakeStampCodeService : IStampCodeProvider
    {
        public string GenerateRedeemCode(Product product)
        {
            return "Redemtion for " + product?.Productname ?? "no product";
        }

        public string GenerateStampCode(IDictionary<Product, int> products)
        {
            return "Generate stamps " + 
                products?
                    .Aggregate("", (codestr, productEntry)
                        => codestr + $"{productEntry.Key?.Productname ?? "N.DEF"}:{productEntry.Value}"
                    )
                ?? "no products";
        }
    }
}
