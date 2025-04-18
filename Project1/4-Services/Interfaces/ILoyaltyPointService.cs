using Project1.DTOs;
using Project1.Models;
namespace Project1.Services;

public interface ILoyaltyPointService
{

    Task<decimal> CalculateTotalLoyaltyPoints(Guid id);
    void SetLoyaltyStatus(Customer customer);

}