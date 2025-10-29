using Domain.Core.Models;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ToolDbContext : IdentityDbContext<IdentityUser>
    {
        public ToolDbContext(DbContextOptions<ToolDbContext> options) : base(options)
        {
        }

        public DbSet<Tool> Tools { get; set; }
        public DbSet<Category> ToolCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            SeedIdentityData(modelBuilder);
        }

        private void SeedIdentityData(ModelBuilder modelBuilder)
        {

            //configure entity properties and relationships using Fluent API
            modelBuilder.Entity<RentalDetail>(rd =>
            {
                rd.HasKey(x => x.Id);
                rd.Property(x => x.Quantity).IsRequired();
                rd.Property(x => x.PricePerDay).IsRequired();
                rd.Property(x => x.TotalPrice).IsRequired();

                rd.HasOne(x => x.Rental)
                  .WithMany(r => r.RentalDetails)
                  .HasForeignKey(x => x.RentalId);

                rd.HasOne(x => x.Tool)
                  .WithMany()
                  .HasForeignKey(x => x.ToolId);
            });

            modelBuilder.Entity<Customer>(c =>
            {
                c.HasKey(x => x.CustomerId);
                c.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                c.Property(x => x.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                c.Property(x => x.Email).HasMaxLength(255).IsRequired();
                c.Property(x => x.PhoneNumber).IsRequired();
                c.Property(x => x.Address).HasMaxLength(500);
                c.HasIndex(x => x.Email).IsUnique();

                //relation to Rental
                c.HasMany(x => x.Rentals)
                 .WithOne(r => r.Customer)
                 .HasForeignKey(r => r.CustomerId);
            });

            modelBuilder.Entity<Tool>(t =>
            {
                t.HasKey(x => x.ToolId);
                t.Property(x => x.Name).IsRequired().HasMaxLength(100);
                t.Property(x => x.Description).HasMaxLength(300);
                t.Property(x => x.PricePerDay).IsRequired();
                t.Property(x => x.QuantityInStock).IsRequired();

                t.HasOne(x => x.Category)
                 .WithMany(c => c.Tools)
                 .HasForeignKey(x => x.CategoryId);
            });

            modelBuilder.Entity<Category>(tc =>
            {
                tc.HasKey(x => x.CategoryId);
                tc.Property(x => x.Name).IsRequired().HasMaxLength(100);
                tc.Property(x => x.Description).HasMaxLength(500);

                tc.HasMany(x => x.Tools)
                  .WithOne(t => t.Category)
                  .HasForeignKey(t => t.CategoryId);
            });

            modelBuilder.Entity<Rental>(r =>
            {
                r.HasKey(x => x.RentalId);
                r.Property(x => x.RentalDate).IsRequired();
                r.Property(x => x.ReturnDate);
                r.Property(x => x.IsReturned).IsRequired();
                r.Property(x => x.TotalQuantity).IsRequired();
                r.Property(x => x.TotalPrice).IsRequired();

                //relation to Customer
                r.HasOne(x => x.Customer)
                 .WithMany(c => c.Rentals)
                 .HasForeignKey(x => x.CustomerId);

                //relation to RentalDetails
                r.HasMany(x => x.RentalDetails)
                 .WithOne(rd => rd.Rental)
                 .HasForeignKey(rd => rd.RentalId);
            });

            SeedData(modelBuilder);
            SeedRoles(modelBuilder);
        }


        private void SeedRoles(ModelBuilder modelBuilder)
        {
            // Seed roller
            var adminRole = new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "STATIC-ADMIN-ROLE-CONCURRENCYSTAMP"
            };

            var userRole = new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "STATIC-USER-ROLE-CONCURRENCYSTAMP"
            };

            modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);

            // Seed användare med statiska lösenordshashes
            var adminUser = new IdentityUser
            {
                Id = "a1",
                UserName = "admin@tool.local",
                NormalizedUserName = "ADMIN@TOOL.LOCAL",
                Email = "admin@tool.local",
                NormalizedEmail = "ADMIN@TOOL.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-ADMIN-SECURITYSTAMP",
                ConcurrencyStamp = "STATIC-ADMIN-CONCURRENCYSTAMP",
                // Passward: Admin123!
                PasswordHash = "AQAAAAIAAYagAAAAEHfdPaPu3AoXt73wEtI9kk74dORAiPsgVJVbKJDfU6UNi2wjuO11LCYGHrCwUxlthQ=="
            };

            var regularUser = new IdentityUser
            {
                Id = "u1",
                UserName = "user@customer.local",
                NormalizedUserName = "USER@CUSTOMER.LOCAL",
                Email = "user@customer.local",
                NormalizedEmail = "USER@CUSTOMER.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-USER-SECURITYSTAMP",
                ConcurrencyStamp = "STATIC-USER-CONCURRENCYSTAMP",
                // Passward: User123!
                PasswordHash = "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g=="
            };

            modelBuilder.Entity<IdentityUser>().HasData(adminUser, regularUser);

            //var hasher = new PasswordHasher<IdentityUser>(); // Create a hasher instance eller hashar ett lösenord

            // assing roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> 
                { 
                    UserId = "a1", 
                    RoleId = "1" 
                }, // Admin -> Admin role
                new IdentityUserRole<string> 
                { 
                    UserId = "u1", 
                    RoleId = "2" 
                }   // User -> User role  
            );
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category
               {
                   CategoryId = 1,
                   Name = "Power Tools",
                   Description = "Electric and battery powered tools for construction and DIY projects"
               },
               new Category
               {
                   CategoryId = 2,
                   Name = "Hand Tools",
                   Description = "Manual tools and basic equipment"
               },
               new Category
               {
                   CategoryId = 3,
                   Name = "Garden Tools",
                   Description = "Outdoor and gardening equipment for lawn and landscape maintenance"
               },
               new Category
               {
                   CategoryId = 4,
                   Name = "Construction Tools",
                   Description = "Heavy duty construction and renovation equipment"
               },
               new Category
               {
                   CategoryId = 5,
                   Name = "Cleaning Equipment",
                   Description = "Professional cleaning tools and pressure washing equipment"
               }
            );

            // Seed tools with realistic data
            modelBuilder.Entity<Tool>().HasData(
                // Power Tools (CategoryId = 1)
                new Tool
                {
                    ToolId = 1,
                    Name = "Cordless Drill 18V",
                    Description = "Professional 18V cordless drill with two batteries and charger",
                    PricePerDay = 15.99m,
                    QuantityInStock = 3,
                    CategoryId = 1
                },
                new Tool
                {
                    ToolId = 2,
                    Name = "Circular Saw 7.25\"",
                    Description = "Heavy duty circular saw with carbide blade",
                    PricePerDay = 22.50m,
                    QuantityInStock = 2,
                    CategoryId = 1
                },
                new Tool
                {
                    ToolId = 3,
                    Name = "Angle Grinder 4.5\"",
                    Description = "Compact angle grinder for cutting and grinding",
                    PricePerDay = 18.99m,
                    QuantityInStock = 4,
                    CategoryId = 1
                },
                new Tool
                {
                    ToolId = 4,
                    Name = "Jigsaw Variable Speed",
                    Description = "Variable speed jigsaw with orbital action",
                    PricePerDay = 16.75m,
                    QuantityInStock = 5,
                    CategoryId = 1
                },
                new Tool
                {
                    ToolId = 5,
                    Name = "Impact Driver",
                    Description = "High torque impact driver with quick-change chuck",
                    PricePerDay = 19.99m,
                    QuantityInStock = 5,
                    CategoryId = 1
                },
                new Tool
                {
                    ToolId = 6,
                    Name = "Orbital Sander",
                    Description = "Random orbital sander with dust collection",
                    PricePerDay = 24.99m,
                    QuantityInStock = 2,
                    CategoryId = 1
                },

                // Hand Tools (CategoryId = 2)
                new Tool
                {
                    ToolId = 7,
                    Name = "Professional Hammer Set",
                    Description = "3-piece hammer set: claw, ball peen, and sledge",
                    PricePerDay = 8.50m,
                    QuantityInStock = 6,
                    CategoryId = 2
                },
                new Tool
                {
                    ToolId = 8,
                    Name = "Socket Wrench Set",
                    Description = "42-piece metric and imperial socket set with ratchets",
                    PricePerDay = 12.99m,
                    QuantityInStock = 3,
                    CategoryId = 2
                },
                new Tool
                {
                    ToolId = 9,
                    Name = "Precision Screwdriver Set",
                    Description = "20-piece precision screwdriver set with magnetic tips",
                    PricePerDay = 6.75m,
                    QuantityInStock = 8,
                    CategoryId = 2
                },
                new Tool
                {
                    ToolId = 10,
                    Name = "Professional Tool Box",
                    Description = "Large tool box with multiple compartments and wheels",
                    PricePerDay = 14.50m,
                    QuantityInStock = 2,
                    CategoryId = 2
                },
                new Tool
                {
                    ToolId = 11,
                    Name = "Adjustable Wrench Set",
                    Description = "4-piece adjustable wrench set (6\", 8\", 10\", 12\")",
                    PricePerDay = 9.99m,
                    QuantityInStock = 4,
                    CategoryId = 2
                },

                // Garden Tools (CategoryId = 3)
                new Tool
                {
                    ToolId = 12,
                    Name = "Self-Propelled Lawn Mower",
                    Description = "21-inch self-propelled gas lawn mower with mulching capability",
                    PricePerDay = 35.99m,
                    QuantityInStock = 2,
                    CategoryId = 3
                },
                new Tool
                {
                    ToolId = 13,
                    Name = "Gas Leaf Blower",
                    Description = "Powerful gas-powered leaf blower with variable speed",
                    PricePerDay = 24.99m,
                    QuantityInStock = 3,
                    CategoryId = 3
                },
                new Tool
                {
                    ToolId = 14,
                    Name = "Electric Hedge Trimmer",
                    Description = "24-inch electric hedge trimmer with rotating handle",
                    PricePerDay = 21.50m,
                    QuantityInStock = 3,
                    CategoryId = 3
                },
                new Tool
                {
                    ToolId = 15,
                    Name = "Chainsaw 16\"",
                    Description = "Professional 16-inch gas chainsaw with safety features",
                    PricePerDay = 42.99m,
                    QuantityInStock = 1,
                    CategoryId = 3
                },
                new Tool
                {
                    ToolId = 16,
                    Name = "String Trimmer",
                    Description = "Gas-powered string trimmer with dual line head",
                    PricePerDay = 28.75m,
                    QuantityInStock = 3,
                    CategoryId = 3
                },

                // Construction Tools (CategoryId = 4)
                new Tool
                {
                    ToolId = 17,
                    Name = "Wet Tile Saw 7\"",
                    Description = "Professional wet tile saw with diamond blade",
                    PricePerDay = 55.99m,
                    QuantityInStock = 1,
                    CategoryId = 4
                },
                new Tool
                {
                    ToolId = 18,
                    Name = "Portable Concrete Mixer",
                    Description = "3.5 cubic feet portable concrete mixer",
                    PricePerDay = 68.50m,
                    QuantityInStock = 1,
                    CategoryId = 4
                },
                new Tool
                {
                    ToolId = 19,
                    Name = "Electric Jackhammer",
                    Description = "35 lb electric demolition hammer with accessories",
                    PricePerDay = 89.99m,
                    QuantityInStock = 5,
                    CategoryId = 4
                },
                new Tool
                {
                    ToolId = 20,
                    Name = "Floor Sander",
                    Description = "Professional drum floor sander for hardwood floors",
                    PricePerDay = 75.25m,
                    QuantityInStock = 1,
                    CategoryId = 4
                },
                new Tool
                {
                    ToolId = 21,
                    Name = "Reciprocating Saw",
                    Description = "Heavy duty reciprocating saw with multiple blades",
                    PricePerDay = 32.99m,
                    QuantityInStock = 2,
                    CategoryId = 4
                },

                // Cleaning Equipment (CategoryId = 5)
                new Tool
                {
                    ToolId = 22,
                    Name = "Electric Pressure Washer",
                    Description = "3000 PSI electric pressure washer with multiple nozzles",
                    PricePerDay = 45.99m,
                    QuantityInStock = 2,
                    CategoryId = 5
                },
                new Tool
                {
                    ToolId = 23,
                    Name = "Professional Carpet Cleaner",
                    Description = "Hot water extraction carpet cleaning machine",
                    PricePerDay = 39.99m,
                    QuantityInStock = 1,
                    CategoryId = 5
                },
                new Tool
                {
                    ToolId = 24,
                    Name = "Shop Vacuum 16 Gallon",
                    Description = "16-gallon wet/dry shop vacuum with multiple attachments",
                    PricePerDay = 18.75m,
                    QuantityInStock = 4,
                    CategoryId = 5
                },
                new Tool
                {
                    ToolId = 25,
                    Name = "Floor Buffer",
                    Description = "20-inch floor buffer/polisher with pads",
                    PricePerDay = 52.50m,
                    QuantityInStock = 1,
                    CategoryId = 5
                }
            );

        }
    }
}
