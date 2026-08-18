using BillingApi.DTOs;
using BillingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService service;

    public InvoiceController(IInvoiceService _service)
    {
        service = _service;
    }

    [HttpGet]
    public async Task<ActionResult<List<InvoiceDto>>> getAll()
    {
        var invoices = await service.getAll();

        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> getById(Guid id)
    {
        var invoice = await service.getById(id);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> create(CreateInvoiceDto dto)
    {
        var invoice = await service.create(dto);

        return CreatedAtAction(
            nameof(getById),
            new { id = invoice.Id },
            invoice
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceDto>> update(Guid id, UpdateInvoiceDto dto)
    {
        var invoice = await service.update(id, dto);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPost("{id}/print")]
    public async Task<IActionResult> print(Guid id)
    {
        var invoice = await service.print(id);

        if (invoice == null)
            return NotFound();

        return Ok(invoice);
    }
}
