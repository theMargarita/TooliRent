using Domain.Core.Core_Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Mapping;
using Services.Service_Interfaces;
using Services.Services;
using Services.Validators;
//using Services.Mapping;

namespace TooliRent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database connection
            builder.Services.AddDbContext<ToolDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Repository patterns
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Servie patterns
            builder.Services.AddScoped<IToolService, ToolService>();
            builder.Services.AddScoped<IGategoryService, CategoryService>();

            //FluentValidation
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateToolValidator>();

            //AutoMapper
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            //CORS
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //              .AllowAnyMethod()
            //              .AllowAnyHeader();
            //    });
            //});

            //Identity
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
            //{
            //    option.Password.RequireDigit = true;
            //    option.Password.RequireLowercase = true;
            //    option.Password.RequireUppercase = true;
            //    option.Password.RequireNonAlphanumeric = false;
            //    option.Password.RequiredLength = 6;
            //    option.User.RequireUniqueEmail = true;
            //})
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ToolDbContext>()
            //    .AddSignInManager()
            //    .AddDefaultTokenProviders();


            //Authentication JWT


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
