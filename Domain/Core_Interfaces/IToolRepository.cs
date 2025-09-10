using Infrastructure.Models;

namespace Domain.Core.Core_Interfaces
{
    public interface IToolRepository : IRepository<Tool>
    {
        //Task<IEnumerable<Tool>> GetToolsWithDetailsAsync();
        Task<IEnumerable<Tool>> GetAvailableToolsAsync();
        Task<IEnumerable<Tool>> SearchToolsAsync(string searchTerm); //search by name, description, category
        Task<int> GetTotalToolsAsync();
        Task<IEnumerable<Tool>> GetToolsByCategoryAsync(string category);
        Task<IEnumerable<Tool>> GetToolsByPriceAsync(decimal minPrice, decimal maxPrice);
        Task<bool>ExistsAsync(string name);

    }
}
