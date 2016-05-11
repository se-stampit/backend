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
        /// Adds a scannable scancode with the respective gain of the stamp to the storage
        /// </summary>
        /// <param name="code">The code for which the stamp can be granted</param>
        /// <param name="products">The products to be granted as stamp for exchanging the code</param>
        void AddStampcode(string code, IDictionary<Product, int> products);
        void AddRedemtioncode(string code, Product product);
        /// <summary>
        /// Returns wheather the code exists as redemtioncode or stampcode
        /// </summary>
        /// <param name="code">The code to check wheather it exists or not</param>
        /// <returns>True if the code exists either in the redemtioncodeStorage or in the stampcodeStorage</returns>
        bool ExistsCode(string code);
    }
}