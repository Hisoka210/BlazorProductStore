using BlazorProductStore.DAL;
using BlazorProductStore.DAL.Models;

namespace BlazorProductStore.BLL
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(string connectionString)
        {
            _categoryRepository = new CategoryRepository(connectionString);
        }

        public List<Category> GetCategories()
        {
            // Business logic could go here (e.g., logging, caching)
            return _categoryRepository.GetAll();
        }

        public void AddCategory(Category category)
        {
            // Example business rule: Ensure category name is not a duplicate before adding.
            var existingCategories = _categoryRepository.GetAll();
            if (existingCategories.Any(c => c.CategoryName.Equals(category.CategoryName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("A category with this name already exists.");
            }

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }

            _categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int categoryId)
        {
            // Example business rule: You might want to check if any products
            // are associated with this category before allowing deletion.
            _categoryRepository.Delete(categoryId);
        }
    }
}