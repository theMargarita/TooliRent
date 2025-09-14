namespace Services.DTOs.RentalDtos
{
    public record RentalDTO //detailed info for rental listings - combines customer and tool info - like a receipt?
    {
        public DateOnly RentalDate { get; init; }
        public DateOnly? ReturnDate { get; init; }
        public decimal TotalCost { get; init; }

        //customer info
        public int CustomerId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }

        //tool info
        public string ToolName { get; init; } = string.Empty;
        public decimal ToolPricePerDay { get; init; }
    }
}
