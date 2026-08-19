using BillingApi.DTOs;

namespace BillingApi.Clients;

public interface IStockClient{
Task<StockProductResponse?> getProductById(Guid id);

  Task debit(DebitStockRequestDto dto);

}