using Domain.Core.Models;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ToolContext : DbContext
    {
        public ToolContext(DbContextOptions<ToolContext> options) : base(options)
        {
        }

        public DbSet<Tools> Tools { get; set; }
        public DbSet<ToolCategory> ToolCategories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customers>(c =>
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

            });

            modelBuilder.Entity<Tools>(t =>
            {
                t.Property(x => x.Name).IsRequired().HasMaxLength(100);
                t.Property(x => x.Description).HasMaxLength(300);
                t.Property(x => x.Price).IsRequired();
                t.Property(x => x.QuantityInStock).IsRequired();


            });

            modelBuilder.Entity<ToolCategory>(tc =>
            {
                tc.Property(x => x.Name).IsRequired().HasMaxLength(100);
                tc.Property(x => x.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<Rental>(r =>
            {
                r.Property(x => x.RentalDate).IsRequired();
                r.Property(x => x.ReturnDate);
                r.Property(x => x.IsReturned).IsRequired();
                r.Property(x => x.TotalQuantity).IsRequired();
                r.Property(x => x.TotalPrice).IsRequired();
            });

            SeedData(modelBuilder);
        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToolCategory>().HasData(
               new ToolCategory
               {
                   CategoryId = 1,
                   Name = "Power Tools",
                   Description = "Electric and battery powered tools for construction and DIY projects"
               },
               new ToolCategory
               {
                   CategoryId = 2,
                   Name = "Hand Tools",
                   Description = "Manual tools and basic equipment"
               },
               new ToolCategory
               {
                   CategoryId = 3,
                   Name = "Garden Tools",
                   Description = "Outdoor and gardening equipment for lawn and landscape maintenance"
               },
               new ToolCategory
               {
                   CategoryId = 4,
                   Name = "Construction Tools",
                   Description = "Heavy duty construction and renovation equipment"
               },
               new ToolCategory
               {
                   CategoryId = 5,
                   Name = "Cleaning Equipment",
                   Description = "Professional cleaning tools and pressure washing equipment"
               }
            );

            // Seed data for your Tools class with QuantityInStock
            modelBuilder.Entity<Tools>().HasData(
                // Power Tools (CategoryId = 1)
                new Tools
                {
                    ToolId = 1,
                    Name = "Cordless Drill 18V",
                    Description = "Professional 18V cordless drill with two batteries and charger",
                    Price = 15.99m,
                    QuantityInStock = 3,
                    CategoryId = 1
                },
                new Tools
                {
                    ToolId = 2,
                    Name = "Circular Saw 7.25\"",
                    Description = "Heavy duty circular saw with carbide blade",
                    Price = 22.50m,
                    QuantityInStock = 2,
                    CategoryId = 1
                },
                new Tools
                {
                    ToolId = 3,
                    Name = "Angle Grinder 4.5\"",
                    Description = "Compact angle grinder for cutting and grinding",
                    Price = 18.99m,
                    QuantityInStock = 4,
                    CategoryId = 1
                },
                new Tools
                {
                    ToolId = 4,
                    Name = "Jigsaw Variable Speed",
                    Description = "Variable speed jigsaw with orbital action",
                    Price = 16.75m,
                    QuantityInStock = 0, // Out of stock
                    CategoryId = 1
                },
                new Tools
                {
                    ToolId = 5,
                    Name = "Impact Driver",
                    Description = "High torque impact driver with quick-change chuck",
                    Price = 19.99m,
                    QuantityInStock = 5,
                    CategoryId = 1
                },
                new Tools
                {
                    ToolId = 6,
                    Name = "Orbital Sander",
                    Description = "Random orbital sander with dust collection",
                    Price = 24.99m,
                    QuantityInStock = 2,
                    CategoryId = 1
                },

                // Hand Tools (CategoryId = 2)
                new Tools
                {
                    ToolId = 7,
                    Name = "Professional Hammer Set",
                    Description = "3-piece hammer set: claw, ball peen, and sledge",
                    Price = 8.50m,
                    QuantityInStock = 6,
                    CategoryId = 2
                },
                new Tools
                {
                    ToolId = 8,
                    Name = "Socket Wrench Set",
                    Description = "42-piece metric and imperial socket set with ratchets",
                    Price = 12.99m,
                    QuantityInStock = 3,
                    CategoryId = 2
                },
                new Tools
                {
                    ToolId = 9,
                    Name = "Precision Screwdriver Set",
                    Description = "20-piece precision screwdriver set with magnetic tips",
                    Price = 6.75m,
                    QuantityInStock = 8,
                    CategoryId = 2
                },
                new Tools
                {
                    ToolId = 10,
                    Name = "Professional Tool Box",
                    Description = "Large tool box with multiple compartments and wheels",
                    Price = 14.50m,
                    QuantityInStock = 2,
                    CategoryId = 2
                },
                new Tools
                {
                    ToolId = 11,
                    Name = "Adjustable Wrench Set",
                    Description = "4-piece adjustable wrench set (6\", 8\", 10\", 12\")",
                    Price = 9.99m,
                    QuantityInStock = 4,
                    CategoryId = 2
                },

                // Garden Tools (CategoryId = 3)
                new Tools
                {
                    ToolId = 12,
                    Name = "Self-Propelled Lawn Mower",
                    Description = "21-inch self-propelled gas lawn mower with mulching capability",
                    Price = 35.99m,
                    QuantityInStock = 2,
                    CategoryId = 3
                },
                new Tools
                {
                    ToolId = 13,
                    Name = "Gas Leaf Blower",
                    Description = "Powerful gas-powered leaf blower with variable speed",
                    Price = 24.99m,
                    QuantityInStock = 3,
                    CategoryId = 3
                },
                new Tools
                {
                    ToolId = 14,
                    Name = "Electric Hedge Trimmer",
                    Description = "24-inch electric hedge trimmer with rotating handle",
                    Price = 21.50m,
                    QuantityInStock = 0, // Out of stock
                    CategoryId = 3
                },
                new Tools
                {
                    ToolId = 15,
                    Name = "Chainsaw 16\"",
                    Description = "Professional 16-inch gas chainsaw with safety features",
                    Price = 42.99m,
                    QuantityInStock = 1,
                    CategoryId = 3
                },
                new Tools
                {
                    ToolId = 16,
                    Name = "String Trimmer",
                    Description = "Gas-powered string trimmer with dual line head",
                    Price = 28.75m,
                    QuantityInStock = 3,
                    CategoryId = 3
                },

                // Construction Tools (CategoryId = 4)
                new Tools
                {
                    ToolId = 17,
                    Name = "Wet Tile Saw 7\"",
                    Description = "Professional wet tile saw with diamond blade",
                    Price = 55.99m,
                    QuantityInStock = 1,
                    CategoryId = 4
                },
                new Tools
                {
                    ToolId = 18,
                    Name = "Portable Concrete Mixer",
                    Description = "3.5 cubic feet portable concrete mixer",
                    Price = 68.50m,
                    QuantityInStock = 1,
                    CategoryId = 4
                },
                new Tools
                {
                    ToolId = 19,
                    Name = "Electric Jackhammer",
                    Description = "35 lb electric demolition hammer with accessories",
                    Price = 89.99m,
                    QuantityInStock = 0, // Out of stock
                    CategoryId = 4
                },
                new Tools
                {
                    ToolId = 20,
                    Name = "Floor Sander",
                    Description = "Professional drum floor sander for hardwood floors",
                    Price = 75.25m,
                    QuantityInStock = 1,
                    CategoryId = 4
                },
                new Tools
                {
                    ToolId = 21,
                    Name = "Reciprocating Saw",
                    Description = "Heavy duty reciprocating saw with multiple blades",
                    Price = 32.99m,
                    QuantityInStock = 2,
                    CategoryId = 4
                },

                // Cleaning Equipment (CategoryId = 5)
                new Tools
                {
                    ToolId = 22,
                    Name = "Electric Pressure Washer",
                    Description = "3000 PSI electric pressure washer with multiple nozzles",
                    Price = 45.99m,
                    QuantityInStock = 2,
                    CategoryId = 5
                },
                new Tools
                {
                    ToolId = 23,
                    Name = "Professional Carpet Cleaner",
                    Description = "Hot water extraction carpet cleaning machine",
                    Price = 39.99m,
                    QuantityInStock = 1,
                    CategoryId = 5
                },
                new Tools
                {
                    ToolId = 24,
                    Name = "Shop Vacuum 16 Gallon",
                    Description = "16-gallon wet/dry shop vacuum with multiple attachments",
                    Price = 18.75m,
                    QuantityInStock = 4,
                    CategoryId = 5
                },
                new Tools
                {
                    ToolId = 25,
                    Name = "Floor Buffer",
                    Description = "20-inch floor buffer/polisher with pads",
                    Price = 52.50m,
                    QuantityInStock = 1,
                    CategoryId = 5
                }
            );
        }
    }
}
