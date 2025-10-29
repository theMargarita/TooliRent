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

                // Update rental detail with tool info
                var rentalDetail = rental.RentalDetails.FirstOrDefault(d => d.ToolId == detailDto.ToolId);

                if (rentalDetail is not null)
                {
                    rentalDetail.PricePerDay = tool.PricePerDay;
                    rentalDetail.RentalId = rental.RentalId;

                    var rentalDays = CalculateRentalDays(rental.RentalDate, rental.ReturnDate);
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

            return _mapper.Map<RentalReadDto>(rental);
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
            var rentals = await _unitOfWork.Rentals.GetAllRentalAsync();

            var rentalDtos = _mapper.Map<IEnumerable<RentalReadDto>>(rentals);

            foreach (var rd in rentalDtos)
            {
                rd.TotalRentedDays = CalculateRentalDays(rd.StartDate, rd.EndDate);
            }

            return rentalDtos;
        }

        private static int CalculateRentalDays(DateOnly startDate, DateOnly? returnDate)
        {
            // If no return date, assume rental is ongoing and use today+1 to ensure at least 1 day
            var endDate = returnDate ?? DateOnly.FromDateTime(DateTime.Today).AddDays(1);

            if (endDate <= startDate)
            {
                throw new ArgumentException("Return date must be the same as or after the start date.");
            }

            var rentalDays = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;

            // Ensure minimum of 1 day
            return rentalDays <= 0 ? 1 : rentalDays;
        }

        public async Task<RentalReadDto> GetRentalById(int id)
        {
            var rental = await _unitOfWork.Rentals.GetRentalByIdAsync(id);

            return _mapper.Map<RentalReadDto>(rental);
        }

        public async Task<bool> UpdateRental(int id, RentalUpdateDto rentalUpdateDto)
        {
            var rentalExists = await _unitOfWork.Rentals.ExistsAsync(id);

            if (rentalUpdateDto.IsReturned)
            {
                Console.WriteLine("Tools is now returned");
            }

            if (!rentalExists) return false;

            await _unitOfWork.Rentals.UpdateAsync(_mapper.Map<Rental>(rentalUpdateDto));
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
