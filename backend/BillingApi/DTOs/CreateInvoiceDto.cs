using System.ComponentModel.DataAnnotations;
using BillingApi.Enums;

namespace BillingApi.DTOs;

public class CreateInvoiceDto
{
    [Required]
    public List<CreateInvoiceItemDto> Items { get; set; } = new();
}
