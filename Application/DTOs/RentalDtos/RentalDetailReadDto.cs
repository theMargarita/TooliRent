namespace Services.DTOs.RentalDtos
{
    public class RentalDetailReadDto
    {
        public int ToolId { get; set; }
        public string ToolName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
        public int RentalDays { get; set; }
        public decimal TotalPrice { get; set; }
    }
}