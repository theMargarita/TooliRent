namespace Services.DTOs.CustomerDtos
{
    public record CustomerDTO
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public DateOnly? BithDate { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Address { get; init; }


    }
}
