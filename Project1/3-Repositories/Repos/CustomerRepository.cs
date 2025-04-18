using Microsoft.EntityFrameworkCore;
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

    public async Task<Customer> PostCustomer(Customer toAdd)
    {
        _context.Customers.Add(toAdd);
        await _context.SaveChangesAsync();

        return toAdd;

    }
    public async Task<Customer> UpdateCustomer(Customer toUpdate)
    {
        _context.Customers.Patch(toUpdate);
        await _context.SaveChangesAsync();

        return toUpdate;

    }

    public async Task<List<Customer>> PostListOfCustomer(List<Customer> customers)
    {

        await _context.Customers.AddRangeAsync(customers);
        await _context.SaveChangesAsync();

        return customers;

    }

    public async Task<Customer?> GetCustomer(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }


    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<bool> DeleteCustomer(Guid id)
    {
        var customer = await GetCustomer(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer); 
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}





