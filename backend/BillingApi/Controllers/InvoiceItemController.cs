using BillingApi.DTOs;
using BillingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceItemController : ControllerBase
{
    private readonly IInvoiceItemService service;

    public InvoiceItemController(IInvoiceItemService _service)
    {
        service = _service;
    }

    [HttpGet]
    public async Task<ActionResult<List<InvoiceItemDto>>> getAll()
    {
        var items = await service.getAll();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceItemDto>> getById(Guid id)
    {
        var item = await service.getById(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceItemDto>> create(CreateInvoiceItemDto dto)
    {
        var item = await service.create(dto);

        return CreatedAtAction(
            nameof(getById),
            new { id = item.Id },
            item
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceItemDto>> update(Guid id, CreateInvoiceItemDto dto)
    {
        var item = await service.update(id, dto);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}
