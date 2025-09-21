using Infrastructure.Models;

namespace Services.DTOs.CategoryDto
{
    public class CategoryReadDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //navigation property
        //public List<Tool> Tools { get; set; } = new List<Tool>();
        public int ToolId { get; set; } //not sure about this one
        public string ToolName { get; set; } = string.Empty;

    }
}
