using Project1.DTOs;
using Project1.Models;
namespace Project1.Services;

public interface ILoyaltyBenefitsService{
    Task<ReceiptDTO> CalculateRebate(Receipt receipt);

}