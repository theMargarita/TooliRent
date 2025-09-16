using Services.DTOs.RentalDtos;

namespace Services.Service_Interfaces
{
    public interface IRentalService
    {
        Task<RentalReadDto> GetRentalById(int id);
        Task<IEnumerable<RentalReadDto>> GetAllRentals();
        Task<RentalReadDto> CreateRental(RentalCreateDto rentalCreateDto);
        Task<bool> UpdateRental(int id, RentalUpdateDto rentalUpdateDto);
        Task<bool> DeleteRental(int id);
    }
}
