namespace Project1.Models;
public class Store{
    //Unique id is based on Company + Number
    public string Company {get; set; }
    public int StoreNumber {get; set; }
    public string Address {get; set; }

    public string PhoneNumber {get; set;}

    public (string, int) StoreKey => (Company, StoreNumber);


    public Store(string company , int number, string address, string phone){

        Company = company;
        StoreNumber = number;
        Address = address;

        PhoneNumber = phone;

    }

    //Many to 1
    public List<Employee> Employees { get; set; } = new List<Employee>();
    public List<Customer> Customers { get; set; } = new List<Customer>();


}