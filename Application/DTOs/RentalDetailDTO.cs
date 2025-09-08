using System.ComponentModel.DataAnnotations.Schema;

namespace Services.DTOs
{
    public record RentalDetailDTO //details for each tool in a rental - if you rent 3 different tools, there will be 3 RentalDetail records
    {
        public int RentalId { get; set; }
        public int ToolId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
