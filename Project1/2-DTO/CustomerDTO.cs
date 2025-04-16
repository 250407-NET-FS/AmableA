using Project1.Models;

namespace Project1.DTOs;

public class CustomerDTO
{   
    public required Guid Id { get; set;}
    public string? FName { get; set; }
    public string? MName { get; set; }
    public string? LName { get; set; }
    public string? PhoneNumber { get; set; }

    public LoyaltyStatus? LoyaltyStatus { get; set; }
}