namespace StockApi.DTOs;

public class ProductDto
{
    public long Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Balance { get; set; }
}