using Microsoft.EntityFrameworkCore;
using Project_s24787.Models;

namespace Project_s24787.Context;


public class ABCDbContext : DbContext
{
    protected ABCDbContext()
    {
    }

    public ABCDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Firm> Firms { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Software> SoftwareSystems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(new List<Category>()
        {
            new()
            {
                IdCategory = 1,
                CategoryName = "finanse"
            },
            new()
            {
                IdCategory = 2,
                CategoryName = "edukacja"
            }
        });

        modelBuilder.Entity<Client>().HasData(new List<Client>()
        {
            new()
            {
                IdClient = 1,
                Address = "Random Street, 1",
                Email = "firm1.email@email.email",
                PhoneNumber = "48123456789"
            },
            new()
            {
                IdClient = 2,
                Address = "Random Street, 2",
                Email = "ind1.email@email.email",
                PhoneNumber = "987654321"
            },
            new()
            {
                IdClient = 3,
                Address = "Random Street, 3",
                Email = "firm2.email@email.email",
                PhoneNumber = "48783490128"
            },
            new()
            {
                IdClient = 4,
                Address = "Random Street, 4",
                Email = "ind2.email@email.email",
                PhoneNumber = "154807654"
            },
        });
        
        modelBuilder.Entity<Discount>().HasData(new List<Discount>()
        {
            new()
            {
                IdDiscount = 1,
                DiscountName = "Welcome back",
                Offer = "Zniżka dla powracających klientów",
                Value = "5%",
                ActiveFrom = new DateTime(2024, 06, 28)
            },
            new()
            {
                IdDiscount = 2,
                DiscountName = "Black Friday Discount",
                Offer = "Zniżka na subskrypcję",
                Value = "10%",
                ActiveFrom = new DateTime(2024, 01, 01),
                ActiveTo = new DateTime(2024, 03, 03)
            }
        });

        modelBuilder.Entity<Firm>().HasData(new List<Firm>()
        {
            new()
            {
                KRSNumber = "123456789",
                IdClient = 1,
                FirmName = "SomeFirmName"
            },
            new()
            {
                KRSNumber = "67854290654327",
                IdClient = 3,
                FirmName = "CoolFirmName"
            }
        });

        modelBuilder.Entity<Individual>().HasData(new List<Individual>()
        {
            new()
            {
                PESEL = "13579087635",
                IdClient = 2,
                Name = "John",
                Surname = "Doe"
            },
            new()
            {
                PESEL = "75648947261",
                IdClient = 4,
                Name = "Jenny",
                Surname = "Davis"
            }
        });

        modelBuilder.Entity<Software>().HasData(new List<Software>()
        {
            new()
            {
                IdSoftware = 1,
                SoftwareName = "FinanceSoftware",
                Description = "Some finance software. Very cool, everyone should buy",
                CurrentVersion = "Version 1.9.7 - prerelease",
                IdCategory = 1,
                LicencePrice = 20000
            },
            new()
            {
                IdSoftware = 2,
                SoftwareName = "EducationSoftware",
                Description = "Very comfortable for teachers. Students will be smart",
                CurrentVersion = "7.9.8",
                IdCategory = 2,
                LicencePrice = 0.0
            }
        });
    }
}