using MongoSample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSample.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product?> GetProductByCodeAsync(string code);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(string code);
    }
}
