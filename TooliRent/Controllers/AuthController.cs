using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service_Interfaces;
using static Services.DTOs.AuthDtos.AuthDto;
using Domain.Core.Core_Interfaces;
using Infrastructure.Models;

namespace TooliRent.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IJwtTokenService jwtTokenService,
            ILogger<AuthController> logger,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Roles = roles
                });
            }

            return Ok(userList);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { Error = "A user with this email already exists" });
            }

            var user = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                EmailConfirmed = true // simplicity
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }

            // Assign role
            IdentityResult roleResult;
            if (registerDto.Email.EndsWith("@toolirent.com", StringComparison.OrdinalIgnoreCase))
            {
                roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation($"User {registerDto.Email} assigned to Admin role automatically");
            }
            else
            {
                roleResult = await _userManager.AddToRoleAsync(user, "User");
                _logger.LogInformation($"User {registerDto.Email} assigned to User role");
            }

            if (!roleResult.Succeeded)
            {
                _logger.LogWarning($"Failed to add user {user.Id} to role");
            }

            // Ensure a Customer row exists and link by email
            var customer = await _unitOfWork.Customers.GetCustomerByEmailAsync(user.Email ?? "");
            if (customer == null)
            {
                var newCustomer = new Customer
                {
                    FirstName = user.UserName ?? user.Email ?? "Customer",
                    LastName = "Customer", // minimal default — consider extending RegisterDto with names
                    Email = user.Email ?? "",
                    PhoneNumber = "" // PhoneNumber is required in model; use empty string if not provided
                };

                await _unitOfWork.Customers.AddAsync(newCustomer);
                await _unitOfWork.SaveChangesAsync();
                customer = newCustomer;
                _logger.LogInformation("Customer row created for user {Email}", user.Email);
            }

            _logger.LogInformation($"User {registerDto.Email} registered successfully");

            return CreatedAtAction(nameof(GetAll), new { Message = "User registered successfully", UserId = user.Id, CustomerId = customer.CustomerId });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new { Error = "Invalid email or password" });
            }

            // Verify password
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    return Unauthorized(new { Error = "Account is locked out" });
                }
                return Unauthorized(new { Error = "Invalid email or password" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            // Find customer by email and include its id as claim in the JWT
            var customer = await _unitOfWork.Customers.GetCustomerByEmailAsync(user.Email ?? "");
            int? customerId = customer?.CustomerId;

            var token = _jwtTokenService.CreateToken(user, roles, customerId);

            _logger.LogInformation("User {Email} logged in successfully", loginDto.Email);

            return Ok(new AuthResponseDto(token, user.UserName ?? "", user.Email ?? "", roles.ToList()));
        }

        [HttpPost("refresh")]
        [Authorize]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RefreshToken()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "Invalid token" });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new { Error = "User not found" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            // Get customer and pass id to token
            var customer = await _unitOfWork.Customers.GetCustomerByEmailAsync(user.Email ?? "");
            int? customerId = customer?.CustomerId;

            var token = _jwtTokenService.CreateToken(user, roles, customerId);

            return Ok(new AuthResponseDto(
                AccessToken: token,
                UserName: user.UserName ?? "",
                Email: user.Email ?? "",
                Roles: roles.ToList()
                ));
        }
    }
}