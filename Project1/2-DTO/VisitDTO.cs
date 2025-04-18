namespace Project1.DTOs;

public class VisitDTO{
    public required Guid Id { get; set; }

    public required Guid CustomerId { get; set; }
    
    public required int StoreId { get; set; }  

    public DateTime? VisitDate { get; set; }  
    public int? PointsAccumulated { get; set; }  
}