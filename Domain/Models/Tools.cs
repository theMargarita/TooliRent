namespace Infrastructure.Models
{
    public class Tools
    {
        public int ToolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; } //create later a tools times days rented to get total price
        public bool IsAvailable { get; set; } = true;
        public DateTime PurchaseDate { get; set; }

        //foreign key
        public int CategoryId { get; set; }

        //navigation property
        public ToolCategory? Category { get; set; }
    }
}
