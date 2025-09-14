using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DTOs.ToolDtos
{
    public record ToolDto
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public int StockQuantity { get; init; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; init; } = 0m;

        //category info
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }
}
