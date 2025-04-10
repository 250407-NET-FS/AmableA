using Library.Repositories;
using Project1.Models;

namespace Project1.Repositories;

public class CustomerRepository : ICustomerRespository
{

    public CustomerRepository()
    {

    }


    public Customer AddCustomer(Customer toAdd){
        return toAdd;

    }

}