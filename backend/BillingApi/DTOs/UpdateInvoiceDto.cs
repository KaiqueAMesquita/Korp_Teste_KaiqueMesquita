using System.ComponentModel.DataAnnotations;
using BillingApi.Enums;

namespace BillingApi.DTOs;

public class UpdateInvoiceDto
{
    public InvoiceStatus Status { get; set; }
    
}
