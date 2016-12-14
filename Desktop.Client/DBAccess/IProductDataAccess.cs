using Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Client.DBAccess
{
    public interface IProductDataAccess
    {
        IList<Product> GetAllProducts();
        Product GetProduct(int id);
        bool AddProduct(Product product);
        bool DeleteProduct(int productId);
        bool EditProduct(int productId, Product product);

        string ErrorMessage { get; }
    }
}
