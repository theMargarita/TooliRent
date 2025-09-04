using Infrastructure.Models;

namespace Domain.Core.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        //public bool IsReturned { get; set; } = false;


        //foreign keys and navigation properties
        public Customers? Customer { get; set; }
        public int CustomerId { get; set; }

        public Tools? Tool { get; set; }
        public int ToolId { get; set; }

    }
}
