using Domain.Core.Core_Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Service_Interfaces;
using Services.Services;
using Services.Validators;
using System.Text;
using TooliRent.WebAPI;

namespace TooliRent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter("yyyy-MM-dd"));
                });

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Write your token here: "
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Database
            builder.Services.AddDbContext<ToolDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ToolDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            // Authentication (JWT)
            var jwt = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Repository patterns
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service patterns
            builder.Services.AddScoped<IToolService, ToolService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation()
                            .AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateToolValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateRentalValidator>();

            // AutoMapper
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
