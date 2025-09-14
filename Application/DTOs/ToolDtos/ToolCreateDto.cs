using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DTOs.ToolDtos
{
    public class ToolCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; }
        public int QuantityInStock { get; set; } = 0;
        public int Category { get; set; } 
    }
}
