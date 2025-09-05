using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } //when returned set to true and make the tool return to its stock quantity
        public int TotalQuantity { get; set; } = 0; //total quantity of tools rented in this transaction
        public decimal TotalPrice { get; set; } = 0m; 


        //foreign keys and navigation properties
        public Customers? Customer { get; set; }
        public int CustomerId { get; set; }

        public Tools? Tool { get; set; }
        public int ToolId { get; set; }

    }
}
