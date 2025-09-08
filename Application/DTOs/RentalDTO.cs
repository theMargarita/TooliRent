namespace Services.DTOs
{
    public record RentalDTO
    {
        public int RentalId { get; init; }
        public DateOnly RentalDate { get; init; }
        public DateOnly? ReturnDate { get; init; }
        public decimal TotalCost { get; init; }

        //customer info
        public int CustomerId { get; init; }
        public string CustomerFirstName { get; init; } = string.Empty;
        public string CustomerLastName { get; init; } = string.Empty;

        //tool info
        public int ToolId { get; init; }
        public string ToolName { get; init; } = string.Empty;
        public decimal ToolPricePerDay { get; init; }
    }
}
