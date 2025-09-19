using Services.DTOs.CustomerDtos;

namespace Services.Service_Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto>CreateCustomerAsync(CustomerCreateDto customerDTO);
        Task<CustomerDto> UpdateCustomerByIdAsync(int id, CustomerUpdateDto customerDTO);
        Task<bool> DeleteCustomerByIdAsync(int id);
        Task<CustomerDto> SortCustomerByNameAsync();
        Task<IEnumerable<CustomerDto>> SearchCustomerAsync(string searchTerm);
    }
}
