using AutoMapper;
using Domain.Core.Core_Interfaces;
using Domain.Core.Models;
using Services.DTOs.RentalDtos;
using Services.Service_Interfaces;

namespace Services.Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RentalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RentalReadDto> CreateRental(RentalCreateDto rentalCreateDto)
        {
            var rental = _mapper.Map<Rental>(rentalCreateDto);

            foreach (var detailDto in rentalCreateDto.RentalDetails)
            {
                var tool = await _unitOfWork.Tools.GetByIdAsync(detailDto.ToolId);

                // Validate tool existence
                if (tool is null)
                {
                    throw new ArgumentException($"Tool with ID {detailDto.ToolId} is not found.");
                }

                // Check stock availability
                if (tool.QuantityInStock < detailDto.Quantity)
                {
                    throw new ArgumentException($"Insufficient stock for tool ID {detailDto.ToolId}\n Requested: {detailDto.Quantity}\n Available: {tool.QuantityInStock}");
                }

                // Map rental detail
                var rentalDetail = rental.RentalDetails.FirstOrDefault(d => d.ToolId == detailDto.ToolId);

                if (rentalDetail is not null)
                {
                    rentalDetail.PricePerDay = tool.PricePerDay;
                    rentalDetail.RentalId = rental.RentalId;

                    var rentalDays = CalculatedRentalDays(rental.RentalDate, rental.ReturnDate);
                    rentalDetail.TotalPrice = rentalDays * detailDto.Quantity * tool.PricePerDay;

                    //remove rented quantity from stock
                    tool.QuantityInStock -= detailDto.Quantity;
                    await _unitOfWork.Tools.UpdateAsync(tool);
                }
            }

            // Calculate total quantity and price for the rental
            rental.TotalQuantity = rental.RentalDetails.Sum(d => d.Quantity);
            rental.TotalPrice = rental.RentalDetails.Sum(d => d.TotalPrice);

            await _unitOfWork.Rentals.AddAsync(rental);
            await _unitOfWork.SaveChangesAsync();

            var createdRental = await _unitOfWork.Rentals.GetByIdAsync(rental.RentalId);

            return _mapper.Map<RentalReadDto>(createdRental);
        }

        private int CalculatedRentalDays(DateOnly startDate, DateOnly? returnDate)
        {
            var endDate = returnDate ?? DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            var startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            var endDateTime = endDate.ToDateTime(TimeOnly.MinValue);

            var rentalDays = (endDateTime - startDateTime).Days;

            return rentalDays == 0 ? 1 : rentalDays; // Minimum of 1 day
        }

        public async Task<bool> DeleteRental(int id)
        {
            var rental = await _unitOfWork.Rentals.ExistsAsync(id);
            if (!rental) return false;

            await _unitOfWork.Rentals.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RentalReadDto>> GetAllRentals()
        {
            var rentals = await _unitOfWork.Rentals.GetAllAsync();

            return _mapper.Map<IEnumerable<RentalReadDto>>(rentals);
        }

        public async Task<RentalReadDto> GetRentalById(int id)
        {
            var rental = await _unitOfWork.Rentals.GetByIdAsync(id);

            return _mapper.Map<RentalReadDto>(rental);
        }

        public async Task<bool> UpdateRental(int id, RentalUpdateDto rentalUpdateDto)
        {
            var rentalExists = await _unitOfWork.Rentals.ExistsAsync(id);

            if (!rentalExists) return false;

            await _unitOfWork.Rentals.UpdateAsync(_mapper.Map<Rental>(rentalUpdateDto));
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
