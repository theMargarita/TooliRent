using Infrastructure.Models;

namespace Services.DTOs.CategoryDto
{
    public class CategoryReadDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
