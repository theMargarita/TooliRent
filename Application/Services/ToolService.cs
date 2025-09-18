using AutoMapper;
using Domain.Core.Core_Interfaces;
using Infrastructure.Models;
using Services.DTOs.ToolDtos;
using Services.Service_Interfaces;

namespace Services.Services
{
    public class ToolService : IToolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToolService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ToolDto> AddToolAsync(ToolCreateDto toolCreateDto)
        {
            var tool = _mapper.Map<Tool>(toolCreateDto);

            await _unitOfWork.Tools.AddAsync(tool);
            await _unitOfWork.SaveChangesAsync();

            // Map the created tool back to ToolDto to return
            var createdTool = _unitOfWork.Tools.GetByIdAsync(tool.ToolId);
            return _mapper.Map<ToolDto>(createdTool);
        }

        public async Task<bool> DeleteToolAsync(int id)
        {
            var toolToDelete = await _unitOfWork.Tools.ExistsAsync(id);

            if (!toolToDelete) return false;

            await _unitOfWork.Tools.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ToolDto>> GetAllToolsAsync()
        {
            var tool = await _unitOfWork.Tools.GetAllAsync();


            return _mapper.Map<IEnumerable<ToolDto>>(tool);
        }

        public async Task<IEnumerable<ToolDto>> GetAvailableToolsAsync()
        {
            var availableTools = await _unitOfWork.Tools.GetAvailableToolsAsync();

            return _mapper.Map<IEnumerable<ToolDto>>(availableTools);
        }

        public async Task<ToolDto> GetToolByIdAsync(int id)
        {
            var tool = await _unitOfWork.Tools.GetByIdAsync(id);

            if(tool is null) return null;

            return _mapper.Map<ToolDto>(tool);
        }

        public async Task<IEnumerable<ToolDto>> GetToolsByPrice(decimal minPrice, decimal maxPrice)
        {
            var tool = await _unitOfWork.Tools.GetToolsByPriceAsync(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ToolDto>>(tool); 
        }

        public async Task<IEnumerable<ToolDto>> SearchToolsAsync(string searchTerm)
        {
            var searchTool = await _unitOfWork.Tools.SearchToolsAsync(searchTerm);

            return _mapper.Map<IEnumerable<ToolDto>>(searchTool);
        }

        public async Task<bool> UpdateToolAsync(ToolUpdateDto toolUpdateDto, int id)
        {
            var toolToUpdate = await _unitOfWork.Tools.GetByIdAsync(id);
            if (toolToUpdate is null) return false;

            _mapper.Map(toolUpdateDto, toolToUpdate);
            await _unitOfWork.Tools.UpdateAsync(toolToUpdate);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<bool>(toolToUpdate);
        }
    }
}
