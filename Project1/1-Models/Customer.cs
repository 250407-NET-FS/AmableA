namespace Project1.Models;
public class Customer{
    public Guid Id { get; set;} = Guid.NewGuid();
    public string? FName { get; set;}
    public string? MName { get; set;}
    public string? LName { get; set;}
    public string? PhoneNumber {get; set;}

    public int LoyaltyPoints;
    public LoyaltyStatus Status;
    //Todo calculate loyalty status based on the amount spent in purchases

    public Customer(){}

    //ToDo refactor it out since theres getter and setters all over
    public Customer(string phone, string first, string last, string? middle = null){
        
        FName = first;
        MName = middle;
        LName = last;
        PhoneNumber = phone;

    }



}