using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Project1.Models;
public class Store
{
    //Unique id is based on Company + Number
    [Key]
    public required int StoreNumber { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }



    public Store() { }
    public Store(int number, string address, string phone)
    {

        StoreNumber = number;
        Address = address;
        PhoneNumber = phone;

    }
}
