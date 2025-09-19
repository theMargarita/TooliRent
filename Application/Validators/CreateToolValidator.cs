using Services.DTOs.ToolDtos;
using FluentValidation;
using FluentValidation.Validators;

namespace Services.Validators
{
    public class CreateToolValidator : AbstractValidator<ToolCreateDto>
    {
        public CreateToolValidator() 
        { 
            //according to swagger this one might have a problem 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tool name is required.")
                .MinimumLength(1).WithMessage("Tool name must be at least 1 character long.")
                .MaximumLength(100).WithMessage("Tool name must not exceed 100 characters.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Tool description is required.").MaximumLength(500).WithMessage("Cannot exceed 500 characters")
                .MinimumLength(30).WithMessage("Must write a proper description");

            RuleFor(x => x.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than zero.")
                .LessThanOrEqualTo(9999.99m).WithMessage("Price per day must have up to 2 decimal places");

            RuleFor(x => x.QuantityInStock).ExclusiveBetween(0, 10).WithMessage("Quantity in stock must be between 0 and 10.");

            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category ID must be a positive integer.");
        }
    }
}
