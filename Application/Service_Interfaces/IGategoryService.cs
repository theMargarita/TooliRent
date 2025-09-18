using Services.DTOs.CategoryDto;

namespace Services.Service_Interfaces
{
    public interface IGategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<IEnumerable<CategoryReadDto>> GetCategoryById(int id);
        //Task CreateCategory(CategoryDTO categoryDto); // think about it
    }
}
