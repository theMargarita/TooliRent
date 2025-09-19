using FluentValidation;
using FluentValidation.Validators;
using Services.DTOs.CustomerDtos;

namespace Services.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CustomerCreateDto>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email format.");

            //regex for international and local phone numbers
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(\+?[0-9]{1,15}|0[0-9]{6,14})$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            // Custom rule to ensure at least one of FirstName or LastName is provided
            RuleFor(x => x).Must(c => !string.IsNullOrWhiteSpace(c.FirstName) || !string.IsNullOrWhiteSpace(c.LastName))
                .WithMessage("At least one of First Name or Last Name must be provided.");

        }
    }
}
