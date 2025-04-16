public interface IReceiptItemRepository
{


    Task PostReceiptItem(Guid receiptId, ReceiptItem receiptItem);

    Task<ReceiptItem> GetReceiptItem(Guid receiptId, string itemName);

}