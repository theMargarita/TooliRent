using Domain.Core.Core_Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Mapping;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database connection
            builder.Services.AddDbContext<ToolDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ToolDbContext>()
            //    .AddDefaultTokenProviders();

            //Repository patterns
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Servie patterns
            //builder.Services.AddScoped<IToolService, ToolService>();

            //FluentValidation

            //AutoMapper
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            //CORS

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
