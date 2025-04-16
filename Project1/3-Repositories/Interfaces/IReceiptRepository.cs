public interface IReceiptRepository{
    Task<Receipt> PostReceipt(Receipt receipt);
    Task<Receipt> PostReceipt(Receipt receipt, List<ReceiptItem> receiptItems);

    Task<List<Receipt>> PostListOfReceipt(List<Receipt> receipts);

    Task<Receipt?> GetReceipt(Guid id);


    Task<List<Receipt>> GetAllReceipts();

    Task<List<Receipt>> GetAllReceiptsByStore(int storeId);

    Task<List<Receipt>> GetAllReceiptsByCustomer(Guid customerId);

    Task<List<Receipt>> GetAllReceiptsByStoreAndCustomer(int storeId, Guid customerId);

    Task<List<Receipt>> GetAllReceiptsByVisit(Guid id);


}