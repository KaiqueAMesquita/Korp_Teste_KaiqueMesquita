using System.ComponentModel.DataAnnotations;

namespace BillingApi.DTOs;

public class CreateInvoiceItemDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
