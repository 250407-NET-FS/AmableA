using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project1.Models;

public class Visit
{

    public Guid Id { get; set; } = Guid.NewGuid();
    

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int StoreId { get; set; }
    public Store Store { get; set; }

    public DateTime VisitDate { get; set; }
    public int PointsAccumulated { get; set; }

    public Visit() { }

}
