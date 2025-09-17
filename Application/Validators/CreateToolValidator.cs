using Services.DTOs.ToolDtos;
using FluentValidation;
using FluentValidation.Validators;

namespace Services.Validators
{
    public class CreateToolValidator : AbstractValidator<ToolCreateDto>
    {
        public CreateToolValidator() 
        { 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tool name is required.")
                .MaximumLength(100).WithMessage("Tool name must not exceed 100 characters.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Tool description is required.");

            RuleFor(x => x.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than zero.")
                .ExclusiveBetween(1, 18000).WithMessage("Price per day must have up to 2 decimal places and a maximum of 18 digits.");

            RuleFor(x => x.QuantityInStock).ExclusiveBetween(0, 10).WithMessage("Quantity in stock must be between 0 and 10.");

            RuleFor(x => x.Category).GreaterThan(0).WithMessage("Category ID must be a positive integer.");
        }
    }
}
