using BillingApi.Enums;

namespace BillingApi.DTOs;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    public long Number { get; set; }

    public InvoiceStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<InvoiceItemDto> Items { get; set; } = new();
}
