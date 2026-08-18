using Microsoft.AspNetCore.Mvc;
using StockApi.DTOs;
using StockApi.Services;

namespace StockApi.Controllers;

[ApiController]
[Route("api/stock")]
public class StockController : ControllerBase
{
    private readonly IStockService service;

    public StockController(IStockService _service)
    {
        service = _service;
    }

    [HttpPost("debit")]
    public async Task<IActionResult> debit(
        [FromBody] DebitStockRequestDto dto)
    {
        await service.debit(dto);

        return NoContent();
    }
}