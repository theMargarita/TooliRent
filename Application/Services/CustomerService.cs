using AutoMapper;
using Domain.Core.Core_Interfaces;
using Infrastructure.Models;
using Services.DTOs.CustomerDtos;
using Services.Service_Interfaces;

namespace Services.Services
{
    //only for admin to use
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //exept for this one maybe
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

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();

            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer is null) return null;

            return _mapper.Map<CustomerDto>(customer);
        }

        //will come back to this one later - need to think about it
        public async Task<IEnumerable<CustomerDto>> SearchCustomerAsync(string searchTerm)
        {
            var customers = _unitOfWork.Customers.SearchCustomersAsync(searchTerm);

            if(customers is null) return null;

            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        //not sure if the method name is correct for the functionality
        public async Task<CustomerDto> SortCustomerByNameAsync()
        {
            var customerToSort = await _unitOfWork.Customers.GetCustomersWithAscendingOrder();

            return _mapper.Map<CustomerDto>(customerToSort);
        }

        public async Task<CustomerDto> UpdateCustomerByIdAsync(int id, CustomerUpdateDto customerDTO)
        {
            var customerToUpdate = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customerToUpdate is null) return null;

            _mapper.Map(customerDTO, customerToUpdate);
            await _unitOfWork.Customers.UpdateAsync(customerToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customerToUpdate);
        }
    }
}
