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

        public Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryReadDto>> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
