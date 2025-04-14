using Library.Repositories;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;


namespace Project1.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio-code

    public async  Task<Customer> PostCustomer(Customer toAdd)
    {
        _context.Customers.Add(toAdd);
        await _context.SaveChangesAsync();

        return toAdd;

    }

    public async Task<Customer> GetCustomer(Guid id){
        return await _context.Customers.FindAsync(id);
    }

}