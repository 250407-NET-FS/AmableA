using Microsoft.EntityFrameworkCore;

namespace Project1.Models;
public class Customer{
    public Guid Id { get; set;} = Guid.NewGuid();
    public string? FName { get; set;}
    public string? MName { get; set;}
    public string? LName { get; set;}
    public string? PhoneNumber {get; set;}

    public int LoyaltyPoints {get; set;} = 0;
    public LoyaltyStatus Status {get; set;}= LoyaltyStatus.Wood;
    //Todo calculate loyalty status based on the amount spent in purchases

    public Customer(){}



}