using Domain.Core.Core_Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ToolRepository : Repository<Tool>, IToolRepository
    {
        public ToolRepository(ToolDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(string name, int id)
        {
            return await _dbSet.AnyAsync(e => e.Name == name || e.ToolId == id);
        }

        public async Task<IEnumerable<Tool>> GetAvailableToolsAsync()
        {
            return await _dbSet
                .Where(t => t.IsAvailable)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tool>> GetToolsByCategoryAsync(string category)
        {
            return await _dbSet
                .Include(t => t.Category)
                .Where(t => t.Category.Name == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tool>> GetToolsByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet.Include(t => t.Category)
                .Where(t => t.PricePerDay >= minPrice && t.PricePerDay <= maxPrice)
                .ToListAsync();
        }

        public Task<int> GetTotalToolsAsync()
        {
            return _dbSet.CountAsync();
        }

        public async Task<IEnumerable<Tool>> SearchToolsAsync(string searchTerm)
        {
            return await _dbSet
                .Include(t => t.Category)
                .Where(t => t.Name.Contains(searchTerm.ToLower()) ||
                            t.Description.Contains(searchTerm.ToLower()) ||
                            t.Category.Name.Contains(searchTerm.ToLower()))
                .ToListAsync();
        }
    }
}
