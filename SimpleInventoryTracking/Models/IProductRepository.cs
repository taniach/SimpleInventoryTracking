using System.Collections.Generic;

namespace SimpleInventoryTracking.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductByProductCode(string productCode);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(string productCode);
    }
}
