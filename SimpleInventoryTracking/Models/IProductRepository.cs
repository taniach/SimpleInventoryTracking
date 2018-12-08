﻿using System.Collections.Generic;

namespace SimpleInventoryTracking.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(string ownerId);

        Product GetProductByProductCode(string productCode, string ownerId);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(string productCode);
    }
}
