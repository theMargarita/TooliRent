using Services.DTOs.CustomerDtos;

namespace Services.Service_Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
        Task<CustomerDTO>CreateCustomerAsync(CustomerCreateDto customerDTO);
        Task<CustomerDTO> UpdateCustomerByIdAsync(int id, CustomerUpdateDto customerDTO);
        Task<CustomerDTO> DeleteCustomerByIdAsync(int id);
        Task<CustomerDTO> SortCustomerByNameAsync(string ascName, string descName);
        Task<IEnumerable<CustomerDTO>> SearchCustomerAsync(string searchTerm);
    }
}
