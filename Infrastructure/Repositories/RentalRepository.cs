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
        public RentalRepository(ToolContext context) : base(context) 
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
                .Include(r => r.OrderDetails)
                .Where(r => r.OrderDetails
                .Any(rd => rd.CustomerId == customerId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalsByToolIdAsync(int toolId)
        {
            return await _dbSet
                .Include(r => r.OrderDetails)
                .Where(od => od.OrderDetails
                .Any(od => od.ToolId == toolId))
                .ToListAsync();
        }

        public Task<IEnumerable<Rental>> GetRentalsWithDetailsAsync() //not sure what i wanted with this so leaving it unimplemented for now
        {
            throw new NotImplementedException(); 
        }

        public Task<int> GetTotalRentalsAsync()
        {
            return _dbSet.CountAsync();
        }
    }
}
