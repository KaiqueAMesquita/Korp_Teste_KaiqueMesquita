using StockApi.DTOs;

namespace StockApi.Services;

public interface IStockService
{
    Task debit(DebitStockRequestDto dto);
}