using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.RentalDtos
{
    public class ReturnRentalDto
    {
        public int RentalId { get; set; }
        public bool IsReturned { get; set; } = true;
        public DateTime RentedAt { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
