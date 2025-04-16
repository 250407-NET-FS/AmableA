using Microsoft.EntityFrameworkCore;
using Project1.Models;


namespace Project1.Repositories;

public class ReceiptItemRepository : IReceiptItemRepository
{

    ApplicationDbContext _context;

    public ReceiptItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //remember to ask if this is proper since I am calling a depencency of receiptitems being receipt and thus making 2 calls
    public async Task<List<ReceiptItem>> PostReceiptItem(Guid receiptId, List<ReceiptItem> receiptItem)
    {
        Receipt? receipt = await _context.Receipts.FindAsync(receiptId);

        if (receipt == null)
        {
            throw new InvalidOperationException("Receipt not found.");
        }

        foreach (ReceiptItem item in receiptItem)
        {
            item.ReceiptId = receiptId;
        }




        await _context.ReceiptItems.AddRangeAsync(receiptItem);
        await _context.SaveChangesAsync();



        return receiptItem;

    }
    public async Task<ReceiptItem?> GetReceiptItem(Guid receiptId, string itemName)
    {
        return await _context.ReceiptItems.FirstOrDefaultAsync(r => r.ReceiptId == receiptId && r.ItemName == itemName);

    }
}

