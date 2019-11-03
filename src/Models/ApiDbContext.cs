using System;
using Microsoft.EntityFrameworkCore;

namespace MediatrSampleApi.Models
{
    /// <summary>
    /// </summary>
    public class ApiDbContext : DbContext
    {
        /// <summary>
        /// </summary>
        public ApiDbContext(DbContextOptions<ApiDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        /// <summary>
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            var customerId = Guid.NewGuid();
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = customerId,
                Name = "Salih Igde",
                Email = "salihigde@gmail.com",
                CreatedDate = DateTime.Now
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedDate = DateTime.Now,
                Price = 1000
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedDate = DateTime.Now,
                Price = 1200
            });
        }
	}
}
