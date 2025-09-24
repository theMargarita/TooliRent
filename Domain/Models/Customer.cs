using Domain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Customer //: IdentityUser 
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        //navigation property
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
