namespace Project1.Models;
public class Employee
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string FName { get; set; } = string.Empty;
    public string? MName { get; set; }
    public string LName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    //One to one, not null
    public Store Store;

    public Employee() {}
    public Employee(string phone, string address, Store store, string fName, string lName, string? mName = null)
    {

        if (store == null)
        {
            throw new ArgumentNullException(nameof(store), "Store cannot be null.");
        }

        PhoneNumber = phone;
        FName = fName;
        MName = mName;
        LName = lName;
        Address = address;
        Store = store;

    }

}