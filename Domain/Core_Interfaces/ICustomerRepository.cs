using Domain.Core.Core_Interfaces;
using Infrastructure.Models;

namespace Domain.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersWithAscendingOrder();
        Task<Customer?> GetCustomerByEmailAsync(string email);
        //Task<Customer?> GetCustomerByPhoneAsync(string phoneNumber);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm); //search by name, email, phone
        Task<int> GetTotalCustomersAsync();
        Task DeleteAsync(int id);
    }
}
