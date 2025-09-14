namespace Services.DTOs.RentalDtos
{
    public class RentalReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }

        // Navigation properties
        public List<RentalDetailReadDto> RentalDetails { get; set; } = new List<RentalDetailReadDto>();
    }
}
