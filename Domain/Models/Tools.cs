using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //navigation property
        public ToolCategory Category { get; set; }
        public int CategoryId { get; set; }
    }
}
