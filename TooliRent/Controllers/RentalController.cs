using Domain.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.RentalDtos;
using Services.Service_Interfaces;

namespace TooliRent.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        //[AllowAnonymous]
        // Get all rentals
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<object>>> GetAllRentals()
        {
            var rentals = await _rentalService.GetAllRentals();

            if (rentals == null || !rentals.Any()) return NotFound("No was rentals found.");

            return Ok(rentals);
        }

        // Get rental by id
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<object>> GetRentalById(int id)
        {
            var rental = await _rentalService.GetRentalById(id);
            if (rental == null) return NotFound($"Could not find the id {id}");
            return Ok(rental);
        }

        // Create a new rental
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RentalCreateDto>> CreateRental([FromBody] RentalCreateDto rentalCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdRental = await _rentalService.CreateRental(rentalCreateDto);

            return CreatedAtAction(nameof(GetRentalById), new { id = createdRental.RentalId }, createdRental);
        }

        // Update a rental
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRental(int id, [FromBody] RentalUpdateDto rentalUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updateResult = await _rentalService.UpdateRental(id, rentalUpdateDto);

            if (!updateResult) return NotFound($"Could not find the id {id}");

            return Ok($"Your rental order with the id {id} is now updated");
        }

        // Return a rental
        [HttpPut("{id}/return")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReturnRental(int id)
        {
            var returnResult = await _rentalService.ReturnRental(id);

            if (!returnResult) return NotFound($"Could not find the id {id}");
       
            return Ok($"Your rental order with the id {id} is now returned");
        }

        // Delete a rental
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var deleteResult = await _rentalService.DeleteRental(id);

            if (!deleteResult) return NotFound($"Could not find the id {id}");

            return Ok($"Your rental is now deleted: {deleteResult}");
        }
    }
}
