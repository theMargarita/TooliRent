namespace Services.DTOs.RentalDtos
{
    public class RentalUpdateDto
    {
        public DateOnly? NewReturnDate { get; set; }

        //when time passes the tool automatically returns
        //public bool IsReturned { get; set; } = true;
        //public decimal? TotalPrice { get; set; }
    }
}
