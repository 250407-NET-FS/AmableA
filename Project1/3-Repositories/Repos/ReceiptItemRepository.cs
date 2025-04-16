using System.Data.Entity;

namespace Project1.Repositories;

public class ReceiptItemRepository : IReceiptItemRepository
{

    ApplicationDbContext _context;

    public ReceiptItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //remember to ask if this is proper since I am calling a depencency of receiptitems being receipt and thus making 2 calls
    public async Task PostReceiptItem(Guid receiptId, ReceiptItem receiptItem)
    {
        Receipt receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == receiptId);

        if (receipt == null)
        {
            throw new InvalidOperationException("Receipt not found.");
        }

        receipt.ReceiptItem.Add(receiptItem);
        await _context.SaveChangesAsync();

    }
    public async Task<ReceiptItem> GetReceiptItem(Guid receiptId, string itemName)
    {
        return await _context.ReceiptItems.FirstOrDefaultAsync(r => r.ReceiptId == receiptId && r.ItemName == itemName);

    }
}

