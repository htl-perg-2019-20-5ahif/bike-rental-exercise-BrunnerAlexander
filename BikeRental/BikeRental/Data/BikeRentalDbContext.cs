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
        }
    }
}
