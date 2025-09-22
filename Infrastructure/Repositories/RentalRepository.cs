using Domain.Core.Core_Interfaces;
using Domain.Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(ToolDbContext context) : base(context) 
        { }

        public async Task<IEnumerable<Rental>> GetActiveRentalsAsync() 
        {
            return await _dbSet
                .Where(r => !r.IsReturned)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllRentalAsync()
        {
            return await _dbSet
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.Tool)
                        .ThenInclude(t => t.Category)
                .ToListAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.Tool)
                        .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(r => r.RentalId == id);
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(r => r.RentalDetails)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public Task<int> GetTotalRentalsAsync()
        {
            return _dbSet.CountAsync();
        }
    }
}
