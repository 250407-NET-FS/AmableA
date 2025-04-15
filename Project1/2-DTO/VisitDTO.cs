namespace Project1.DTOs;

public class VisitDTO{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    
    public int StoreId { get; set; }  

    public DateTime VisitDate { get; set; }  
    public int PointsAccumulated { get; set; }  
}