using StockApi.DTOs;
using StockApi.Entities;
using StockApi.Repositories;

namespace StockApi.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository repository;

    public ProductService(IProductRepository _repository)
    {
        repository = _repository;
    }

    public async Task<ProductDto> create(CreateProductDto dto)
    {
        var product = new Product
        {
            Code = dto.Code,
            Description = dto.Description,
            Balance = dto.Balance
        };
        var createdProduct = await repository.create(product);

        return MapToDto(createdProduct);
    }

    private ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Code = product.Code,
            Description = product.Description,
            Balance = product.Balance
        };
    }

    public async Task<List<ProductDto>> getAll()
    {
        var products = await repository.getAll();

         return products.Select(product => MapToDto(product)).ToList();

    }

    public async Task<ProductDto?> getById(Guid id)
    {
        var product = await repository.getById(id);

        if(product == null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");

        }

        return MapToDto(product);
    }

    public async Task<ProductDto> update(Guid id, CreateProductDto dto)
    {
        var product = await repository.getById(id);

        if(product == null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        } 
        product.Code =  dto.Code;
        product.Description = dto.Description;
        product.Balance = dto.Balance;

        await repository.saveChanges();

        return MapToDto(product);

    }
}