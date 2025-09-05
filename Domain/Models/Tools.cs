namespace Infrastructure.Models
{
    public class Tools
    {
        public int ToolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0m; 
        public int QuantityInStock { get; set; } = 0;
        public bool IsAvailable => QuantityInStock > 0;
        //public DateTime PurchaseDate { get; set; }

        //foreign key
        public int CategoryId { get; set; }

        //navigation property
        public ToolCategory? Category { get; set; }
    }
}
