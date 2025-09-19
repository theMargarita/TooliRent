using Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; } //that is the product id
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; } = 0m; 
        public int QuantityInStock { get; set; }
        public bool IsAvailable => QuantityInStock > 0;

        //navigation property
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
