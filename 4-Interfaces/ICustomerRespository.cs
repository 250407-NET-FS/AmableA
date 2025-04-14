using Project1.Models;
namespace Library.Repositories;

//for DI
public interface ICustomerRespository{

    Customer AddCustomer(Customer toAdd);
}