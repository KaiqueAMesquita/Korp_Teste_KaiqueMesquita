namespace BillingApi.Entities;

public class InvoiceItem
{
    public Guid Id {get; set;} = Guid.NewGuid();
    public required int ProductId {get; set;}

    public required string ProductCode {get; set;}

    public required string ProductDescription {get; set;}

    public required int Quantity {get; set;}

    public Guid InvoiceId {get;set;}

}