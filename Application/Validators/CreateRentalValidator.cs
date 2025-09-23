using FluentValidation;
using Services.DTOs.RentalDtos;

namespace Services.Validators


{
    public class CreateRentalValidator : AbstractValidator<RentalCreateDto>
    {
        public CreateRentalValidator()
        {
            RuleFor(x => x.StartDate)
               .NotEmpty().WithMessage("Rental date is required.");
               //.LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
               //.WithMessage("Rental date cannot be in the future.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Return date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("Return date must be after the rental date.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");
                
        }
    }
}
