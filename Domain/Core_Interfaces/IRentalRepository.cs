using Domain.Core.Models;

namespace Domain.Core.Core_Interfaces
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<IEnumerable<Rental>> GetAllRentalAsync();
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<int> GetTotalRentalsAsync();
        Task<IEnumerable<Rental>> GetActiveRentalsAsync();
        Task<IEnumerable<Rental>> GetRentalsByCustomerIdAsync(int customerId);
    }
}