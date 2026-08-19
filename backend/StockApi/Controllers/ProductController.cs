using StockApi.DTOs;
using StockApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace StockApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService service;

    public ProductController(IProductService _service)
    {
        service = _service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> getAll()
    {
        var products = await service.getAll();

        return Ok(products);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> getById(Guid id)
    {
        var product = await service.getById(id);

        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> create(CreateProductDto dto)
    {
        var product = await service.create(dto);

        return CreatedAtAction(
            nameof(getById),
            new {id = product.Id},
            product
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> update(Guid id, CreateProductDto dto)
    {
        var product = await service.update(id,dto);

        if(product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }




    
}