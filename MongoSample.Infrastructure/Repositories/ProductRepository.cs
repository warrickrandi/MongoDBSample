using MongoDB.Driver;
using MongoSample.Domain.Entities;
using MongoSample.Domain.Interfaces;
using MongoSample.Infrastructure.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSample.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDBContext _dbContext;

        public ProductRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            product.Id = null;
            product = await _dbContext.InsertDocument<Product>(product);

            return product;
        }

        public async Task DeleteProductAsync(string code)
        {
            var filter = Builders<Product>.Filter.Eq("ProductCode", code);

            var product = await _dbContext.GetCollection<Product>().Find(filter).FirstOrDefaultAsync();
            if (product != null)
            {
                await _dbContext.DeleteDocument(product);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _dbContext.GetCollection<Product>().AsQueryable().ToList();
        }

        public async Task<Product?> GetProductByCodeAsync(string code)
        {
            var filter = Builders<Product>.Filter.Eq("ProductCode", code);

            return await _dbContext.GetCollection<Product>().Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq("ProductCode", product.ProductCode);


            var productFiltered = await _dbContext.GetCollection<Product>().Find(filter).FirstOrDefaultAsync();

            product.Id = productFiltered.Id;

            await _dbContext.UpdateDocument(product);
        }
    }
}
