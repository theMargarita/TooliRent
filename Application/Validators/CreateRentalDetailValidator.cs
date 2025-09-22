using FluentValidation;
using Services.DTOs.RentalDtos;

namespace Services.Validators
{
    public class CreateRentalDetailValidator : AbstractValidator<RentalDetailCreateDto>
    {
        public CreateRentalDetailValidator() 
        { 
            RuleFor(x => x.ToolId)
                .NotEmpty().WithMessage("Tool ID is required.")
                .GreaterThan(0).WithMessage("Tool ID must be a positive integer.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be a positive integer.");
        }
    }
}
