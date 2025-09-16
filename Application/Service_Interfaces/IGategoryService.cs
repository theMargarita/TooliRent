using Services.DTOs.CategoryDto;
using Services.DTOs.RentalDtos;

namespace Services.Service_Interfaces
{
    public interface IGategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<IEnumerable<CategoryReadDto>> GetCategoryById(int id);
        //Task CreateCategory(CategoryDto categoryDto); // think about it
    }
}
