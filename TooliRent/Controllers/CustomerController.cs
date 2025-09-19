using Microsoft.AspNetCore.Mvc;
using Services.DTOs.CustomerDtos;
using Services.Service_Interfaces;

namespace TooliRent.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        //comments is just to find the endpoints easier
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //get all customers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();

            if (customers is null || !customers.Any()) return NotFound("No customers found.");

            return Ok(customers);
        }

        //get customer by id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null) return NotFound($"Could not find the id {id}");

            return Ok(customer);
        }

        //create customer
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdCustomer = await _customerService.CreateCustomerAsync(customerCreateDto);

            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        //update customer
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedCustomer = await _customerService.UpdateCustomerByIdAsync(id, customerUpdateDto);

            if (updatedCustomer == null) return NotFound($"Could not find the id {id}");

            return Ok($"This tool has now been updated: {updatedCustomer}");
        }

        //delete customer
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deletedCustomer = await _customerService.DeleteCustomerByIdAsync(id);

            if (!deletedCustomer) return NotFound($"Could not find the id {id}");

            return Ok($"This tool has now been updated: {id}");
        }
    }
}
