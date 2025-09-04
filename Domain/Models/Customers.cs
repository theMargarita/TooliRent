using Domain.Core.Models;

namespace Infrastructure.Models
{
    public class Customers
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly? BithDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        //public DateTime CreatedAt { get; set; } //ai suggested it but not sure if needed now 
        //public DateTime UpdatedAt { get; set; }

        //navigation property
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
