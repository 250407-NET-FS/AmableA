
using Project1.DTOs;
using Project1.Models;
namespace Project1.Services;

public interface IReceiptService
{

    Receipt CalculateTotalReceiptsPrice(Receipt receipt, List<ReceiptItem> receiptItems);
    Task<ReceiptDTO> SetReceipt(Receipt receipt);

}