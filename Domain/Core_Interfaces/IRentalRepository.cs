using Domain.Core.Models;

namespace Domain.Core.Core_Interfaces
{
    public interface IRentalRepository
    {
        // Add methods specific to Rental entity if needed
        Task<IEnumerable<Rental>> GetRentalsWithDetailsAsync();
        Task<int> GetTotalRentalsAsync();
        Task<IEnumerable<Rental>> GetActiveRentalsAsync();
        Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Rental>> GetRentalsByToolIdAsync(int toolId);

    }
}