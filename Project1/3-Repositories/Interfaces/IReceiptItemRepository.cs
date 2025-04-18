using Project1.Models;

public interface IReceiptItemRepository
{


    Task<List<ReceiptItem>> PostReceiptItem(Guid receiptId, List<ReceiptItem> receiptItem);

    Task<ReceiptItem?> GetReceiptItem(Guid receiptId, string itemName);

    Task<bool> DeleteReceiptItem(Guid receiptId, string itemName);

}