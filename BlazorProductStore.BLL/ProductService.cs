using BlazorProductStore.DAL;
using BlazorProductStore.DAL.Models;

namespace BlazorProductStore.BLL
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(string connectionString)
        {
            _productRepository = new ProductRepository(connectionString);
        }

        public List<Product> GetProducts()
        {
            // Here you could add business logic, e.g., logging, validation.
            return _productRepository.GetAll();
        }

        public void AddProduct(Product product)
        {
            // Business rule: Product name cannot be empty.
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                throw new ArgumentException("Product name is required.");
            }
            _productRepository.Add(product);
        }
    }
}