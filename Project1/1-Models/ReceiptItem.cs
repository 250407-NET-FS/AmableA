using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


namespace Project1.Models;

public class ReceiptItem
{
    

    //Since this is a weak entity ill need the foreign keys I want it to make ReceiptGuid and Itemname the keys
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/foreign-and-principal-keys

    

    public required string ItemName { get; set; }
    public Guid? ReceiptId { get; set; }
    
    [JsonIgnore] //otherwise it causes circular error
    public Receipt? Receipt { get; set; }

    

    [Precision(18,2)]
    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public ReceiptItem(){} 




}