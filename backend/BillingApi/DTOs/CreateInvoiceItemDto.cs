using System.ComponentModel.DataAnnotations;

namespace BillingApi.DTOs;

public class CreateInvoiceItemDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public string ProductCode { get; set; } = string.Empty;

    [Required]
    public string ProductDescription { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
