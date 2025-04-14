using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.Repositories;

public interface ICustomerRepository
{
    Task<Customer> PostCustomer(Customer toAdd);
    Task<Customer> GetCustomer(Guid id);

}