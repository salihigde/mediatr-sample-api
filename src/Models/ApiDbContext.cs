using System;
using Microsoft.EntityFrameworkCore;

namespace MediatrSample.Api.Models;

/// <summary>
/// DbContext for the API
/// </summary>
public class ApiDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of <see cref="ApiDbContext"/>.
    /// </summary>
    public ApiDbContext(DbContextOptions<ApiDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    /// <summary>
    /// Gets or sets the Customers DbSet.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Gets or sets the Orders DbSet.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Configures the model.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seeding initial data
        var customerId = Guid.NewGuid();
        modelBuilder.Entity<Customer>().HasData(new Customer
        {
            Id = customerId,
            Name = "Salih Igde",
            Email = "salihigde@gmail.com",
            CreatedDate = DateTime.UtcNow // Use UTC time for consistency
        });

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedDate = DateTime.UtcNow, // Use UTC time for consistency
                Price = 1000
            },
            new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedDate = DateTime.UtcNow, // Use UTC time for consistency
                Price = 1200
            }
        );
    }
}