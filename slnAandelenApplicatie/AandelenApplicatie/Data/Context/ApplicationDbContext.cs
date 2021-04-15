using AandelenApplicatie.Data.Poco;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AandelenApplicatie.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //====Relations
            //Stocks
            modelBuilder.Entity<Stock>()
                //  .HasQueryFilter(Stocks => EF.Property<bool>(Stocks, "IsDeleted") == false)
                .HasOne(x => x.StockList)
                .WithMany(x => x.Stocks)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            //Company
            modelBuilder.Entity<Company>()
                .HasOne(x => x.StockList)
                .WithOne(x => x.Company)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction)
                ;
            //Price
            modelBuilder.Entity<Price>()
                .HasOne(x => x.Stock)
                .WithMany(x => x.Prices)
                .OnDelete(DeleteBehavior.NoAction);
            //Ownership
            modelBuilder.Entity<Ownership>()
                .HasOne(x => x.Stock)
                .WithMany(x => x.Ownership)
                .OnDelete(DeleteBehavior.NoAction);
            //Ownership
            modelBuilder.Entity<Buyer>()
                .HasMany(x => x.Ownerships)
                .WithOne(x => x.Buyer)
                .OnDelete(DeleteBehavior.NoAction);
            //====Data
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "KBC",
                });

            modelBuilder.Entity<StockList>().HasData(
                new StockList()
                {
                    StockListId = 1,
                    CompanyId = 1,
                    Name = "KBC-unam"
                });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ownership> Ownerships { get; set; } //Needs to be at first
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<StockList> StockLists { get; set; }
    }
}