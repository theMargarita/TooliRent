namespace Services.DTOs.RentalDtos
{
    public class RentalReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalRentedDays { get; set; }

        // Navigation properties
        public List<RentalDetailReadDto> RentalDetails { get; set; } = new List<RentalDetailReadDto>();
    }
}
