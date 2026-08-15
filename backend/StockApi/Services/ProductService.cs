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

        return new ProductDto
        {
            Id = createdProduct.Id,
            Code = createdProduct.Code,
            Description = createdProduct.Description,
            Balance = createdProduct.Balance
        };
    }

    public async Task<List<ProductDto>> getAll()
    {
        var products = await repository.getAll();

        return products.Select(product => new ProductDto
        {
            Id = product.Id,
            Code = product.Code,
            Description = product.Description,
            Balance = product.Balance
        }).ToList();

    }

    public async Task<ProductDto?> getById(int id)
    {
        var product = await repository.getById(id);

        if(product == null)
        {
            return null;

        }

        return new ProductDto
        {
            Id = product.Id,
            Code = product.Code,
            Description = product.Description,
            Balance = product.Balance
        };
    }

    public async Task<ProductDto> update(int id, CreateProductDto dto)
    {
        var product = await repository.getById(id);

        if(product == null)
        {
            return null;
        } 
        product.Code =  dto.Code;
        product.Description = dto.Description;
        product.Balance = dto.Balance;

        await repository.saveChanges();

        return new ProductDto
        {
            Id = product.Id,
            Code = product.Code,
            Description = product.Description,
            Balance = product.Balance
        };

        


    }
}