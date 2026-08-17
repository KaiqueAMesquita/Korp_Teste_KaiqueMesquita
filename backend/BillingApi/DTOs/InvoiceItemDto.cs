namespace BillingApi.DTOs;

public class InvoiceItemDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int ProductId { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string ProductDescription { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public Guid InvoiceId {get; set;}
}
