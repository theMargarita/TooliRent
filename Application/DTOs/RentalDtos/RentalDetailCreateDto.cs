namespace Services.DTOs.RentalDtos
{
    public class RentalDetailCreateDto
    {
        public int ToolId { get; set; }
        public int Quantity { get; set; }
        public int RentalDays { get; set; }
    }
}