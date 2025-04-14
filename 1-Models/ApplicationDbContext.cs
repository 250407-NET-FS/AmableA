
using Microsoft.EntityFrameworkCore;
using Project1.Models;

//https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers {get; set;}
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}