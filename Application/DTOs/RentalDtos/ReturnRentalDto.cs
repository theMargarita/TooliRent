namespace Services.DTOs.RentalDtos
{
    public class ReturnRentalDto
    {
        public int RentalId { get; set; }
        public bool IsReturned { get; set; } = true;
        public DateOnly RentedAt { get; set; }
        public DateOnly? ReturnDate { get; set; }

    }
}
