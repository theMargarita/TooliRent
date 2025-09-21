using Domain.Core.Core_Interfaces;
using Domain.Core.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(r => r.RentalDetails)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalsByToolIdAsync(int toolId)
        {
            return await _dbSet
                .Include(r => r.RentalDetails)
                .Where(od => od.RentalDetails
                .Any(od => od.ToolId == toolId))
                .ToListAsync();
        }

        public async Task<Rental?> GetRentalByIdWithDetailsAsync(int id) //not sure what i wanted with this so leaving it unimplemented for now
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    //.Include(rd => rd.Tools)
                .FirstOrDefaultAsync(r => r.RentalId == id);


        }

        public Task<int> GetTotalRentalsAsync()
        {
            return _dbSet.CountAsync();
        }
    }
}
