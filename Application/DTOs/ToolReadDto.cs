namespace Services.DTOs
{
    public class ToolReadDto
    {
        public int ToolId { get; set; } //that is the product id
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; } = 0m;
        public int QuantityInStock { get; set; } = 0;
        public bool IsAvailable => QuantityInStock > 0;
    }
}
