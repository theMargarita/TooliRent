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
        public int CustomerId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; } = 0m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } = 0m;
    }
}
