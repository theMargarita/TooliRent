using Domain.Core.Core_Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ToolDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbSet
                .Include(c => c.Tools)
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoriesWithToolsAsync(string toolName)
        {
            return await _dbSet
                .Include(c => c.Tools)
                .Where(c => c.Tools
                .Any(t => t.Name == toolName))
                .ToListAsync();
        }
        public async Task<Category?> GetCategoryWithToolsById(int categoryId)
        {
            return await _dbSet
                .Include(c => c.Tools)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

    }
}
