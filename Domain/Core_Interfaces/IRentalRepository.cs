using Domain.Core.Models;

namespace Domain.Core.Core_Interfaces
{
    public interface IRentalRepository : IRepository<Rental>
    {
        // Add methods specific to Rental entity if needed
        Task<Rental?> GetRentalByIdWithDetailsAsync(int id);
        Task<int> GetTotalRentalsAsync();
        Task<IEnumerable<Rental>> GetActiveRentalsAsync();
        Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Rental>> GetRentalsByToolIdAsync(int toolId);
    }
}