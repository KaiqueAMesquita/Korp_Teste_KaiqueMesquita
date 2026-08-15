using StockApi.DTOs;

namespace StockApi.Services;

public interface IProductService
{
    Task<List<ProductDto>> getAll();

    Task<ProductDto?> getById(int id);

    Task<ProductDto> create(CreateProductDto dto);

    Task<ProductDto> update(int id,CreateProductDto dto);
    
}
