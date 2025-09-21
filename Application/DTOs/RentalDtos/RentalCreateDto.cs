using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.RentalDtos
{
    public class RentalCreateDto
    {
        public int CustomerId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<RentalDetailCreateDto> RentalDetails { get; set; } = new List<RentalDetailCreateDto>();
    }
}
