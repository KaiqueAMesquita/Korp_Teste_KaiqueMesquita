using System.ComponentModel.DataAnnotations;
using BillingApi.Enums;

namespace BillingApi.DTOs;

public class UpdateInvoiceDto
{
    [Required]
    public long Number { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Open;




}
