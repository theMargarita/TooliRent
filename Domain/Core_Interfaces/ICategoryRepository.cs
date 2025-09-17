using Infrastructure.Models;
using System.Linq.Expressions;

namespace Domain.Core.Core_Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetCategoriesWithToolsAsync(string toolName);
        Task<Category?> GetCategoryWithToolsById(int categoryId);
    }
}
