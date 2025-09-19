using Domain.Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ToolDbContext context) : base(context)
        {
        }
        public async Task DeleteAsync(int id)
        {
           var removedCustomer =  await _dbSet.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (removedCustomer != null)
            {
                _dbSet.Remove(removedCustomer);
            }
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            var customerEmail = await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
            return customerEmail;
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithAscendingOrder()
        {
            return await _dbSet
                .Include(c => c.Rentals)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();
        }

        public async Task<int> GetTotalCustomersAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            return await _dbSet
                .Where(c => c.FirstName.Contains(searchTerm) ||
                            c.LastName.Contains(searchTerm) ||
                            c.Email.Contains(searchTerm) ||
                            c.PhoneNumber.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
