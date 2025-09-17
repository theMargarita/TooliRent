using Services.DTOs.ToolDtos;

namespace Services.Service_Interfaces
{
    public interface IToolService
    {
        Task<IEnumerable<ToolDto>> GetAllToolsAsync();
        Task<ToolDto> GetToolByIdAsync(int id);
        Task<ToolDto> AddToolAsync(ToolCreateDto toolCreateDto);
        Task<bool> UpdateToolAsync(ToolUpdateDto toolUpdateDto, int id);
        Task<bool> DeleteToolAsync(int id);
        Task<IEnumerable<ToolDto>> GetAvailableToolsAsync();
        Task<IEnumerable<ToolDto>> SearchToolsAsync(string searchTerm);
        Task<IEnumerable<ToolDto>> GetToolsByPrice(decimal minPrice, decimal maxPrice);

    }
}
