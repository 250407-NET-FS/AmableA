using System.ComponentModel.DataAnnotations;

namespace Project1.Models;
public class Store{
    //Unique id is based on Company + Number
    [Key]
    public int StoreNumber {get; set; }
    public string Address {get; set; }

    public string PhoneNumber {get; set;}



    public Store(){}
    public Store(int number, string address, string phone){


        StoreNumber = number;
        Address = address;
        PhoneNumber = phone;

    }

    //Many to 1
    public List<Employee> Employees { get; set; } = new List<Employee>();



}