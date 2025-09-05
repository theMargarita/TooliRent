using Domain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Tools
    {
        [Key]
        public int ToolId { get; set; } //that is the product id
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0m; 
        public int QuantityInStock { get; set; } = 0;
        public bool IsAvailable => QuantityInStock > 0;
        //public DateTime PurchaseDate { get; set; }



        //foreign key
        public int CategoryId { get; set; }

        //navigation property
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ToolCategory? Category { get; set; }
    }
}
