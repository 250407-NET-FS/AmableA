using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class ReceiptItem
{
    

    //Since this is a weak entity ill need the foreign keys I want it to make ReceiptGuid and Itemname the keys
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/foreign-and-principal-keys

    

    public required string ItemName { get; set; }
    public required Guid ReceiptId { get; set; }
    public required Receipt Receipt { get; set; }

    

    [Precision(18,2)]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public ReceiptItem(){}




}