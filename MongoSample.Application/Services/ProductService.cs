using MongoSample.Domain.Entities;
using MongoSample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSample.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<Product?> GetProductByCodeAsync(string code)
        {
            return await _productRepository.GetProductByCodeAsync(code);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task UpdateProductAsync(Product updatedProduct)
        { 
            await _productRepository.UpdateProductAsync(updatedProduct);
        }

        public async Task DeleteProductAsync(string code)
        {
            await _productRepository.DeleteProductAsync(code);
        }
    }
}
