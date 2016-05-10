using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// Interface for dealing with stampcodes
    /// </summary>
    public interface IStampCodeProvider
    {
        /// <summary>
        /// Generates a new stampcode for the given products in later exchange
        /// </summary>
        /// <param name="products">A list of keyvalue pairs defining the products with the respective count which can be gained by redeeming the stampcode</param>
        /// <returns>The 20 digit long generated stampcode in hex format</returns>
        string GenerateStampCode(IDictionary<Product,int> products);
        /// <summary>
        /// Generates a new redeemtioncode for a specific product of the company to be used to redeem a full stampcard
        /// </summary>
        /// <param name="product">The product used to specify the stampcard of the user to be redeem</param>
        /// <returns>The 20 digit long generated redeemtioncode in hex format</returns>
        string GenerateRedeemCode(Product product);
    }
}
