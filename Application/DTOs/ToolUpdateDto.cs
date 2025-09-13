using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DTOs
{
    public class ToolUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public int QuantityInStock { get; set; } = 0;
        public int CategoryId { get; set; }
    }
}
