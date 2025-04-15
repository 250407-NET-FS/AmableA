using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class ReceiptItem
{
    

    //Since this is a weak entity ill need the foreign keys 
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/foreign-and-principal-keys
    [Key]
    public Guid Id { get; set; }= Guid.NewGuid();

    [Required]
    public Guid ReceiptGuid;
    

    public required Receipt Receipt;

    public string? ItemName;

    [Precision(18,2)]
    public decimal Price;

    public int Quantity;

    public ReceiptItem(){}




}