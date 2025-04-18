using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project1.DTOs;
using Project1.Models;

namespace Project1.Services;

public class ReceiptService : IReceiptService
{


    private readonly IReceiptRepository _receiptRepository;
    private readonly IReceiptItemRepository _receiptItemRepository;
    private readonly ILoyaltyBenefitsService _loyaltyBenefitsService;

    public ReceiptService(
            IReceiptRepository receiptRepository,
            IReceiptItemRepository receiptItemRepository,
            ILoyaltyBenefitsService loyaltyBenefitsService
    )
    {
        _receiptRepository = receiptRepository;
        _receiptItemRepository = receiptItemRepository;
        _loyaltyBenefitsService = loyaltyBenefitsService;
    }

    public Receipt CalculateTotalReceiptsPrice(Receipt receipt, List<ReceiptItem> receiptItems)
    {

        decimal totalAmount = 0.00M;

        foreach (ReceiptItem item in receiptItems)
        {
            decimal amount = item.Price * item.Quantity;
            totalAmount += amount;
        }

        receipt.TotalAmount = totalAmount;

        return receipt;

    }

    public async Task<ReceiptDTO> SetReceipt(Receipt receipt)
    {
        if (receipt.ReceiptItem.Count > 0)
        {
            //List<ReceiptItem> items = receipt.ReceiptItem;
            //This creates a shallow copy and the clear later will clear it and thus it will be an empty list 
            //So i need to make a deep copy... 2 hours cause i forgot such a simplething....

            List<ReceiptItem> items = receipt.ReceiptItem
            .Select(item => new ReceiptItem
            {
                ItemName = item.ItemName,
                Price = item.Price,
                Quantity = item.Quantity,
                ReceiptId = item.ReceiptId
            }).ToList();


            receipt.ReceiptItem.Clear(); //making it empty so that I can add them separetly

            receipt = CalculateTotalReceiptsPrice(receipt, items);


            var persistedReceipt = await _receiptRepository.PostReceipt(receipt);

            if (persistedReceipt != null)
            {

                var persistedReceiptItem = await _receiptItemRepository.PostReceiptItem(persistedReceipt.Id, items);

                var receiptItemDTOs = items.Select(r => new ReceiptItemDTO
                {
                    ItemName = r.ItemName,
                    Price = r.Price,
                    Quantity = r.Quantity,
                }).ToList();

                ReceiptDTO receiptDTO = await _loyaltyBenefitsService.CalculateRebate(receipt);

                receiptDTO.ReceiptItemDTOs = receiptItemDTOs;

                return receiptDTO;

            }
            else
            {
                return null;
            }
        }
        else
        {
            var persistedReceipt = await _receiptRepository.PostReceipt(receipt);

            ReceiptDTO receiptDTO = new ReceiptDTO
            {
                Id = persistedReceipt.Id,
                VisitId = persistedReceipt.VisitId,
                TotalAmount = persistedReceipt.TotalAmount
            };

            return receiptDTO;
        }
    }
}

