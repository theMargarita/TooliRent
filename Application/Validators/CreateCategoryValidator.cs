using FluentValidation;
using Services.DTOs.CategoryDto;
using System.Data;

namespace Services.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Category description must not exceed 200 characters.");
        }
    }
}
