using Stampit.Entity;
using System.Collections.Generic;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// Defines the interface of the temporary storage of stampcodes
    /// </summary>
    public interface IStampCodeStorage
    {
        /// <summary>
        /// Removes the given code from the storage and returns the type of stamps which are added to the scanner's stampcard
        /// </summary>
        /// <param name="code">The code to be used</param>
        /// <returns>Foreach product the amout of stamps which should be added</returns>
        IDictionary<Product, int> UseStampCode(string code);
        /// <summary>
        /// Removes the given code from the storage and returns the type of stamps which are added to the scanner's stampcard
        /// </summary>
        /// <param name="code">The code to be used</param>
        /// <returns>Foreach product the amout of stamps which should be added</returns>
        Product UseRedemtionCode(string code);
        /// <summary>
        /// Returns wheather the code exists as redemtioncode or stampcode
        /// </summary>
        /// <param name="code">The code to check wheather it exists or not</param>
        /// <returns>True if the code exists either in the redemtioncodeStorage or in the stampcodeStorage</returns>
        bool ExistsCode(string code);
    }
}