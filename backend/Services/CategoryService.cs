using Context;
using Entities;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService
    {
        private readonly eCommerceContext _context;

        public CategoryService(eCommerceContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories?.Any() == true ? categories : null;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (category == null)
                return null;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
            if (category == null)
                return null;

            var products = await _context.Products
                .Where(p => p.CategoryId == category.Id)
                .ToListAsync();

            return products?.Any() == true ? products : null;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category updateCategory)
        {
            if (id != updateCategory.Id)
                return false;

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            category.Name = updateCategory.Name;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
