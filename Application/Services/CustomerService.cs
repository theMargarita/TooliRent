using AutoMapper;
using Domain.Core.Core_Interfaces;
using Infrastructure.Models;
using Services.DTOs.CustomerDtos;
using Services.Service_Interfaces;

namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> DeleteCustomerByIdAsync(int id)
        {
            var customerToDelete = await _unitOfWork.Customers.ExistsAsync(id);

            if (!customerToDelete) return false;

            await _unitOfWork.Customers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDto>> SearchCustomerAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> SortCustomerByNameAsync(string ascName, string descName)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> UpdateCustomerByIdAsync(int id, CustomerUpdateDto customerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
