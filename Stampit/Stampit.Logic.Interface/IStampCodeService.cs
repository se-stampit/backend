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
        Task ScanCodeAsync(string code);
    }
}
