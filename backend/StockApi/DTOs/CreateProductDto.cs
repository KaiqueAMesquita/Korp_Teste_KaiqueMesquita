using System.ComponentModel.DataAnnotations;

namespace StockApi.DTOs;

public class CreateProductDto
{
    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Balance { get; set; }
}