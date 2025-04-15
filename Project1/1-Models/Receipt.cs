using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

public class Receipt{

    

    public Guid Id  { get; set;}= Guid.NewGuid();

    [Required]
    public Guid VisitId { get; set;}
    [Required]
    public Visit Visit { get; set;}
    


    // No store type was specified for the decimal property 'TotalAmount' on entity type 'Receipt'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
    //https://stackoverflow.com/questions/71114340/ef-core-6-decimal-precision-warning

    [Precision(18,2)]
    public decimal TotalAmount {get; set;}



    public Receipt(){}



    
    public List<ReceiptItem> ReceiptItem { get; set;} = [];

}

