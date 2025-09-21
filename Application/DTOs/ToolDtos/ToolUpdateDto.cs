using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DTOs.ToolDtos
{
    public class ToolUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; }
        public int QuantityInStock { get; set; } 
        public int CategoryId { get; set; }
    }
}
