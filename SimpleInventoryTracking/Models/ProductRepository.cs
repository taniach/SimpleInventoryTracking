using System.Collections.Generic;
using System.Linq;

namespace SimpleInventoryTracking.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void DeleteProduct(string productCode)
        {
            _appDbContext.Products.Remove(_appDbContext.Products.Find(productCode));
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts(string ownerId)
        {
            return _appDbContext.Products.Where(p => p.OwnerID.Equals(ownerId));
        }

        public Product GetProductByProductCode(string productCode, string ownerId)
        {
            return _appDbContext.Products.FirstOrDefault
                (p => p.ProductCode.Equals(productCode) && p.OwnerID.Equals(ownerId));
        }

        public void UpdateProduct(Product product)
        {
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }
    }
}
