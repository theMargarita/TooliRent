using FluentValidation;
using Services.DTOs.AuthDtos;

namespace Services.Validators
{
    public class RegisterValidator : AbstractValidator<AuthDto.RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName)
                 .NotEmpty().WithMessage("Username is required")
                 .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                 .MaximumLength(50).WithMessage("Username cannot exceed 50 characters")
                 .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores and hyphens");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Invalid email format")
                 .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }

    public class LoginValidator : AbstractValidator<AuthDto.LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Invalid email format").MinimumLength(5).WithMessage("Email must be at least 5 characters")
                 .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be atlest 6 characters long.");
        }
    }
}
