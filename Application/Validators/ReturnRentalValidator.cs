using FluentValidation;
using Services.DTOs.RentalDtos;

namespace Services.Validators
{
    public class ReturnRentalValidator : AbstractValidator<ReturnRentalDto>
    {
        public ReturnRentalValidator()
        {
            RuleFor(x => x.RentalId)
                .NotEmpty().WithMessage("Rental ID is required.");

            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("Return date is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Return date cannot be in the past.");

            RuleFor(x => x.ReturnDate)
                .GreaterThanOrEqualTo(x => x.ReturnDate)
                .WithMessage("Return date cannot be before the rental date.");
        }
    }
}
