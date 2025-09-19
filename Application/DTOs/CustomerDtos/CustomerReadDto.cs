namespace Services.DTOs.CustomerDtos
{
    public class CustomerReadDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
