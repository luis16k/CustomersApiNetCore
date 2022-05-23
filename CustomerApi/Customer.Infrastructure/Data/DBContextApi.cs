using Customer.Domain;
using Customer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Infrastructure
{
    public class DbContextApi : DbContext
    {
        public DbContextApi(DbContextOptions<DbContextApi> options)
            : base(options)
        {
        }

        public DbSet<CustomerModel> CustomerModel => Set<CustomerModel>();
        public DbSet<PurchaseModel> PurchaseModel => Set<PurchaseModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerModel>().HasData( new CustomerModel() { CustomerId = 1, Name = "Jose" });
            modelBuilder.Entity<PurchaseModel>().HasData(new PurchaseModel() { PurchaseId = 1, Cost = 1000, CustomerId = 1 });

            modelBuilder.Entity<PurchaseModel>()
                .HasOne(p => p.Customer)
                .WithMany(b => b.Purchases)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseModel>().Ignore(x => x.Customer);
        }

    }
}
