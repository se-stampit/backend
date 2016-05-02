using Stampit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stampit.Webapp.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }


        public static implicit operator KeyValuePair<Product, int>(ProductViewModel productViewModel)
        {
            if (productViewModel == null) throw new ArgumentNullException(nameof(productViewModel));

            return new KeyValuePair<Product, int>(productViewModel.Product, productViewModel.Count);
        }
    }
}