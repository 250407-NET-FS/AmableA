namespace Project1.DTOs;

public class ReceiptItemDTO{
    public required string ItemName { get; set; }
    public required Guid ReceiptId { get; set; }
    public required Receipt Receipt { get; set; }

    

    
    public decimal? Price { get; set; }

    public int? Quantity { get; set; }


}