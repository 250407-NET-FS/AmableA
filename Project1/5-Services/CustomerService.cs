using Project1.Models;
using Project1.DTOs;

public class CustomerService : ICustomerService{
    private readonly ICustomerRepository _custmerRepository;

    public CustomerService(
        ICustomerRepository customerRepository
    )
    {
        _custmerRepository = customerRepository;
    }


    // public Customer PostCustomer(CustomerDTO customerDTO){
    //     return
    // }
}