using Services.DTOs;

namespace Services.Service_Interfaces
{
    public interface IToolService
    {
        Task<IEnumerable<ToolDto>> GetAllToolsAsync();
        Task<ToolDto> GetToolByIdAsync(int id);
        Task<ToolDto> AddToolAsync(ToolDto toolDto);
        Task<ToolDto> UpdateToolAsync(ToolDto toolDto);
        Task<bool> DeleteToolAsync(int id);
        Task<IEnumerable<ToolDto>> GetAvailableToolsAsync();
        Task<IEnumerable<ToolDto>> SearchToolsAsync(string searchTerm);

    }
}
