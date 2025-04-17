using Project1.Models;
using Project1.DTOs;

namespace Project1.Services;
public class CustomerService : ICustomerService{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(
        ICustomerRepository customerRepository
    )
    {
        _customerRepository = customerRepository;
    }


    // public Customer PostCustomer(CustomerDTO customerDTO){
    //     return
    // }
}