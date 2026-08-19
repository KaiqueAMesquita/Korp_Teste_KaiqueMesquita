using BillingApi.Enums;

namespace BillingApi.Entities;

public class Invoice
{
    public Guid Id {get; set;}

    public required long Number {get; set;}

    public InvoiceStatus Status {get; set;}

    public DateTime CreatedAt {get; set;}
    
    public List<InvoiceItem> Items {get; set;} = new();

}