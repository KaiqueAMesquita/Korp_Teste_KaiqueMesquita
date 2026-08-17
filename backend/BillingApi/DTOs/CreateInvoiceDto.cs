using System.ComponentModel.DataAnnotations;
using BillingApi.Enums;

namespace BillingApi.DTOs;

public class CreateInvoiceDto
{
    [Required]
    public long Number { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<CreateInvoiceItemDto> Items { get; set; } = new();
}
