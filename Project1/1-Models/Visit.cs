using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project1.Models;

public class Visit
{

    public required Guid Id { get; set; } = Guid.NewGuid();
    

    public required Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public required int StoreId { get; set; }
    public Store Store { get; set; }

    public DateTime? VisitDate { get; set; }
    public int? PointsAccumulated { get; set; }

    public Visit() { }

}
