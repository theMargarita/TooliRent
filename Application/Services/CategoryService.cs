using AutoMapper;
using Domain.Core.Core_Interfaces;
using Services.DTOs.CategoryDto;
using Services.Service_Interfaces;

namespace Services.Services
{
    public class CategoryService : IGategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var category = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(category);
        }

        public async Task<CategoryReadDto> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if(category is null) return null;

            return _mapper.Map<CategoryReadDto>(category);
        }
    }
}
