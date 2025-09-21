using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core.Models
{
    public class RentalDetail
    {
        [Key]
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int ToolId { get; set; }
        public int CustomerId { get; set; } //starting to get a bit unsure with this one
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; } = 0m;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } = 0m;


        // Navigation properties
        public Rental? Rental { get; set; }
        public Tool? Tool { get; set; }
    }
}
