using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// Provides the logic for scanning either a stampcode or a redemptioncode
    /// </summary>
    public interface IStampCodeService
    {
        /// <summary>
        /// Uses the given code and looks up if the code is useable, if so the code is redeemed for granting stamps or redeeming stampcards
        /// </summary>
        /// <param name="code">The stampcode or redemtioncode to be used</param>
        /// <param name="user">The user who scanned the code</param>
        Task ScanCodeAsync(string code, Enduser user);
        /// <summary>
        /// Adds the given stampcode to the scannable scancodes to generate stamps
        /// </summary>
        /// <param name="stampcode">The stampcode to be read to gain the stamps</param>
        /// <param name="products">The products to be gained by scanning the stampcode</param>
        void AddStampcode(string stampcode, IDictionary<Product, int> products);
        /// <summary>
        /// Adds the given stampcode to the redeemable scancodes to redeem a stampcard
        /// </summary>
        /// <param name="stampcode">The stampcode to be read to redeem the associated stampcard</param>
        /// <param name="product">The product for which stampcard should be redeemed</param>
        void AddReedemtionStampcode(string stampcode, Product product);
    }
}
