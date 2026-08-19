using StockApi.DTOs;

namespace StockApi.Services;

public interface IProductService
{
    Task<List<ProductDto>> getAll();

    Task<ProductDto?> getById(Guid id);

    Task<ProductDto> create(CreateProductDto dto);

    Task<ProductDto> update(Guid id,CreateProductDto dto);
    
}
