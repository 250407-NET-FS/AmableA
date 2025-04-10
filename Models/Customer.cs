namespace Project1.Models;
public class Customer{
    public Guid CGuid { get; set;} = Guid.NewGuid();
    public string FName { get; set;} = string.Empty;
    public string MName { get; set;} = string.Empty;
    public string LName { get; set;} = string.Empty;
    public string PhoneNumber {get; set;} = string.Empty;

    public Customer(string phone, string first, string last, string middle = null){
        
        FName = first;
        MName = middle;
        LName = last;
        PhoneNumber = phone;

    }
    //n to n
    public List<Store> Stores  { get; set;} = new List<Store>();


}