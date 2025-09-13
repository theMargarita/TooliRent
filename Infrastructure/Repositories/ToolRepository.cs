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

        public async Task<bool> ExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(e => e.Name == name);
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
                .Include(t => t.Name)
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
                .Where(t => t.Name.Contains(searchTerm) ||
                            t.Description.Contains(searchTerm) ||
                            t.Category.Name.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
