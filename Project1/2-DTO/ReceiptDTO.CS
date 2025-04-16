namespace Project1.DTOs;

public class ReceiptDTO{

    public required Guid Id { get; set; } = Guid.NewGuid();
    public required Guid VisitId { get; set; }
    //since not interacting with database i should be ok not doing precision?
    public decimal? TotalAmount { get; set; }

    public List<ReceiptItemDTO>  ReceiptItemDTOs { get; set; } = [];
}