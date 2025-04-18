namespace Project1.DTOs;

public class ReceiptItemDTO{
    public required string ItemName { get; set; }
    //public  Guid? ReceiptId { get; set; }
    public decimal? Price { get; set; }

    public int? Quantity { get; set; }


}