using BikeRental.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Data
{
    public class BikeRentalDbContext : DbContext
    {
        public BikeRentalDbContext(DbContextOptions<BikeRentalDbContext> options) : base(options)
        { }

        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>()
                .HasMany(b => b.Rentals)
                .WithOne(r => r.Bike);

            modelBuilder.Entity<Bike>()
                .Property(b => b.RentalPriceFirstHour)
                .HasColumnType("smallmoney");

            modelBuilder.Entity<Bike>()
                .Property(b => b.RentalPriceAdditionalHour)
                .HasColumnType("smallmoney");

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .Property(r => r.RentalCost)
                .HasColumnType("smallmoney");
        }
    }
}
