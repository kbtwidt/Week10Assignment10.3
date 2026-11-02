using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Week10Assignment10._3.Models;

namespace Week10Assignment10._3.Data
{
    public class CarContext:DbContext
    {
        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=CCAD23Cars;integrated security=True;encrypt=False;trustservercertificate=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData
                (
                new Car {Id = 1, VIN = "1HGCM82633A004351", Make = "Honda", Model = "Accord", Year = 2022, Price = 26500m, Color = "Silver"},
                new Car {Id = 2, VIN = "1FTFW1E85NFA12345", Make = "Ford", Model = "F-150", Year = 2023, Price = 42500m, Color = "Black"}
                );
        }
    }
}
