using Stampit.CommonType;
using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// The repository of all products
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Finds the products of the given company
        /// </summary>
        /// <param name="company">The company to define which products should be returned</param>
        /// <param name="pagenr">The number of the page which is requested</param>
        /// <param name="pagesize">The size of each page, default value is 10</param>
        /// <returns>The list of the requested products</returns>
        Task<IEnumerable<Product>> FindProductsFromCompany(Company company, int pagenr, int pagesize = Setting.DEFAULT_PAGE_SIZE);

        /// <summary>
        /// Calculates the sales of the products of the given company
        /// </summary>
        /// <param name="company">The company to define which products should be returned</param>
        /// <returns>The dictionary of the requested prodacts and their sales</returns>
        Task<IDictionary<Product, double>> SalesPerProduct(Company company);
    }
}
