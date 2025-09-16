namespace Services.DTOs.RentalDtos
{
    public interface RentalUpdateDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public List<int>? ToolIds { get; set; } // List of Tool IDs to be rented for the customer
        public decimal? TotalPrice { get; set; }
    }
}
