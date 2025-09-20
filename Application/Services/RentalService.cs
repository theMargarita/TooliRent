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

            await _unitOfWork.Rentals.AddAsync(rental);
            await _unitOfWork.SaveChangesAsync();

            var createdRental = await _unitOfWork.Rentals.GetByIdAsync(rental.RentalId);

            return _mapper.Map<RentalReadDto>(rental);
        }

        public async Task<bool> DeleteRental(int id)
        {
            var rental = await _unitOfWork.Rentals.ExistsAsync(id);
            if (!rental)return false;

            await _unitOfWork.Rentals.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RentalReadDto>> GetAllRentals()
        {
            var rentals =  await _unitOfWork.Rentals.GetAllAsync();

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
