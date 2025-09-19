using Infrastructure.Models;
using Services.DTOs.ToolDtos;

namespace Services.DTOs.CategoryDto
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //navigation property
        public List<ToolDto> Tools { get; set; }  = new List<ToolDto>();
        public int ToolId { get; set; } //not sure about this one

    }
}
