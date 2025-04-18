

using Project1.DTOs;
using Project1.Models;

namespace Project1.Services;


public class LoyaltyBenefitsSerivce : ILoyaltyBenefitsService{

    
    private readonly IReceiptRepository _receiptRepository;
    private readonly ILoyaltyPointService _loyaltyPointService;

    public LoyaltyBenefitsSerivce(
        IReceiptRepository receiptRepository,
        ILoyaltyPointService loyaltyPointService
    )
    {
        _receiptRepository = receiptRepository;
        _loyaltyPointService = loyaltyPointService;
    }
    public async Task<ReceiptDTO> CalculateRebate(Receipt receipt){

        Customer customer = await _receiptRepository.GetCustomerFromReceipt(receipt.Id);

        LoyaltyStatus loyaltyStatus = customer.Status;

        decimal totalAmount = (decimal)receipt.TotalAmount;
        
        decimal newAmount = totalAmount;

        if (loyaltyStatus == LoyaltyStatus.Bronze){
            newAmount = totalAmount -(totalAmount * 0.10m);
        }
        else if (loyaltyStatus == LoyaltyStatus.Silver){
            newAmount = totalAmount -(totalAmount * 0.15m);
        }
        else if (loyaltyStatus == LoyaltyStatus.Gold){
            newAmount = totalAmount -(totalAmount * 0.25m);
        }
        else if (loyaltyStatus == LoyaltyStatus.Platinum){
            newAmount = totalAmount -(totalAmount * 0.30m);
        }
        else if (loyaltyStatus == LoyaltyStatus.Diamond){
            newAmount = totalAmount -(totalAmount * 0.40m);
        }


        ReceiptDTO receiptDTO = new ReceiptDTO{

        Id = receipt.Id,
        VisitId = receipt.VisitId,
        TotalAmount = newAmount,
        Rebate = totalAmount - newAmount  
        };

        customer.LoyaltyPoints = (int)await _loyaltyPointService.CalculateTotalLoyaltyPoints(customer.Id);
        
        _loyaltyPointService.SetLoyaltyStatus(customer);
        return receiptDTO;
    } 


}
