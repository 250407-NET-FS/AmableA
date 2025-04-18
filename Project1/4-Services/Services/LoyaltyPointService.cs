
using Project1.Models;
namespace Project1.Services;


public class LoyaltyPointService : ILoyaltyPointService
{
    ICustomerRepository _customerRepostory;
    IReceiptRepository _receiptRepository;


    public LoyaltyPointService(
        ICustomerRepository customerRepostory,
        IReceiptRepository receiptRepository
    )
    {
        _receiptRepository = receiptRepository;
        _customerRepostory = customerRepostory;
    }
    public async Task<decimal> CalculateTotalLoyaltyPoints(Guid id)
    {

        decimal totalPoint = 0.00M;

        var receipts = await _receiptRepository.GetAllReceiptsByCustomer(id);

        foreach (Receipt r in receipts)
        {
            totalPoint += (decimal)r.TotalAmount;
        }



        return totalPoint;
    }

    public void SetLoyaltyStatus(Customer customer)
    {
        int totalPoints = customer.LoyaltyPoints;
        LoyaltyStatus loyaltyLevel;

        if (totalPoints < 100)
        {
            loyaltyLevel = LoyaltyStatus.Wood;
        }
        else if (totalPoints < 250)
        {
            loyaltyLevel = LoyaltyStatus.Bronze;
        }
        else if (totalPoints < 1000)
        {
            loyaltyLevel = LoyaltyStatus.Silver;
        }
        else if (totalPoints < 5000)
        {
            loyaltyLevel = LoyaltyStatus.Gold;
        }
        else if (totalPoints < 10000)
        {
            loyaltyLevel = LoyaltyStatus.Platinum;
        }
        else
        {
            loyaltyLevel = LoyaltyStatus.Diamond;
        }

        customer.Status = loyaltyLevel;
        _customerRepostory.UpdateCustomer(customer);

    }

}
