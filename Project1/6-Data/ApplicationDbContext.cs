
using Microsoft.EntityFrameworkCore;
using Project1.Models;

//https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Store> Stores { get; set; }

    public DbSet<Visit> Visits { get; set; }

    public DbSet<Receipt> Receipts { get; set; }

    public DbSet<ReceiptItem> ReceiptItems { get; set; }



    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }



    //Look at SchoolDemo for seeding example


    // FluentApi since im using foreign keys 
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/foreign-and-principal-keys
}

