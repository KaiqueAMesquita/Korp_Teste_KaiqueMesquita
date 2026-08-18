namespace BillingApi.DTOs;

public class DebitStockItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}