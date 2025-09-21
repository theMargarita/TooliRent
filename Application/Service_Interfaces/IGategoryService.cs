using Services.DTOs.CategoryDto;

namespace Services.Service_Interfaces
{
    public interface IGategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategory();
        Task<CategoryReadDto> GetCategoryById(int id);
        //Task CreateCategory(CategoryDTO categoryDto); // think about it
    }
}
