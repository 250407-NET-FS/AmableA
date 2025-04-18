using Project1.Models;


public interface ICustomerRepository
{
    Task<Customer> PostCustomer(Customer toAdd);
    Task<List<Customer>> PostListOfCustomer(List<Customer> customers);
    Task<Customer?> GetCustomer(Guid id);
    Task<Customer> UpdateCustomer(Customer toUpdate);
    Task<List<Customer>> GetAllCustomers();

    Task<bool> DeleteCustomer(Guid id);
}