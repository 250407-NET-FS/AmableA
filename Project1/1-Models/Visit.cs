using System.ComponentModel.DataAnnotations;
using Project1.Models;

public class Visit
{
    
    public Guid Id { get; set; }= Guid.NewGuid();
    [Required]
    public Guid CustomerId { get; set; }


    [Required]
    public int StoreId { get; set; }
 

    public DateTime VisitDate { get; set; }  
    public int PointsAccumulated { get; set; }  

    public Visit() {}
       
}
